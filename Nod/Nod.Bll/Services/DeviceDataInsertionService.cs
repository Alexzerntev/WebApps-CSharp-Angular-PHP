using Nod.Bll.Interfaces;
using Nod.Dal;
using Nod.Dal.Interfaces;
using Nod.Model;
using Nod.Model.Entities.DeviceEntries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Nod.Bll.Services
{
    public class DeviceDataInsertionService : IDeviceDataInsertionService
    {
        private readonly IDeviceContext _context;
        public DeviceDataInsertionService(IDeviceContext context)
        {
            _context = context;
        }

        public async Task<ConnectionAttribute> InsertAsync(string unparsedData, ConnectionAttribute connectionAttribute)
        {
            var insertedUnparesedData = await _context.UnparsedData.AddOneAsync(new UnparsedData() { ArrivalDateTime = DateTime.Now, Data = unparsedData });
            return await InsertAll(Parse(unparsedData), insertedUnparesedData.Id, connectionAttribute);
        }

        private List<string[]> Parse(string unparsedData)
        {
            var globalValueParts = unparsedData.Split("$", StringSplitOptions.RemoveEmptyEntries);
            List<string[]> finalList = new List<string[]>();

            for (int i = 0; i < globalValueParts.Length; i++)
            {
                finalList.Add(globalValueParts[i].Split(","));
            }

            return finalList;
        }

        private async Task<ConnectionAttribute> InsertAll(List<string[]> parsed, string parentId, ConnectionAttribute connectionAttribute)
        {
            try
            {
                for (int i = 0; i < parsed.Count; i++)
                {
                    switch (Int32.Parse(parsed[i][0]))
                    {
                        case 0:
                            await InsertReset(parsed[i], parentId, connectionAttribute.DeviceId);
                            break;
                        case 1:
                            connectionAttribute = await InsertConnectionAttributes(parsed[i], parentId);
                            break;
                        case 2:
                            await InsertGps(parsed[i], parentId, connectionAttribute.DeviceId);
                            break;
                        case 3:
                            await InsertHardwareStatus(parsed[i], parentId, connectionAttribute.DeviceId);
                            break;
                    }
                }
                return connectionAttribute;

            }
            catch (Exception)
            {
                return connectionAttribute;
            }

        }

        private async Task InsertGps(string[] gpsData, string parentId, string deviceId)
        {
            if (gpsData.Length != 8)
            {
                return;
            }

            var gps = new Gps(
                ParseDateTime(gpsData[1]),
                ParseCoordinate(gpsData[2], true),
                ParseCoordinate(gpsData[3], false),
                ParseFloat(gpsData[4]),
                ParseFloat(gpsData[5]),
                ParseFloat(gpsData[6]),
                ParseHex(gpsData[7]),
                parentId,
                deviceId
                );

            await _context.Gpses.AddOneAsync(gps);
        }

        private async Task<ConnectionAttribute> InsertConnectionAttributes(string[] connectionAttributesData, string parentId)
        {
            if (connectionAttributesData.Length != 3)
            {
                return null;
            }

            int qualityOfService = ParseInt(connectionAttributesData[2]);

            var connectionAttribute = new ConnectionAttribute(
                connectionAttributesData[1],
                qualityOfService,
                parentId
                );

            await _context.ConnectionAttributes.AddOneAsync(connectionAttribute);
            return connectionAttribute;
        }

        private async Task InsertHardwareStatus(string[] hardwareStatusData, string parentId, string deviceId)
        {
            if (hardwareStatusData.Length < 6)
            {
                return;
            }

            var signalList = new List<Signal>();

            if (hardwareStatusData.Length > 5)
            {
                int index = 0;
                for (int i = 6; i < hardwareStatusData.Length; i++)
                {
                    var signal = new Signal(index, ParseHex(hardwareStatusData[i]));
                    signalList.Add(signal);
                    index++;
                }
            }

            var hardwareStatus = new HardwareStatus(
                ParseDateTime(hardwareStatusData[1]),
                ParseHex(hardwareStatusData[2]),
                ParseHex(hardwareStatusData[3]),
                ParseHex(hardwareStatusData[4]),
                ParseInt(hardwareStatusData[5]) == 1 ? true : false,
                signalList,
                parentId,
                deviceId
                );

            await _context.HardwareStatuses.AddOneAsync(hardwareStatus);
        }

        private async Task InsertReset(string[] resetData, string parentId, string deviceId)
        {
            if (resetData.Length != 3)
            {
                return;
            }

            var reset = new Reset(
                (ResetCauses)ParseHex(resetData[1]),
                ParseDateTime(resetData[2]),
                parentId,
                deviceId
                );
            await _context.Resets.AddOneAsync(reset);
        }

        #region Safe Parsers

        private DateTime ParseDateTime(string dateTimeString)
        {
            bool succeed;
            DateTime parsedDateTime;
            succeed = DateTime.TryParseExact(dateTimeString, "ddMMyyHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out parsedDateTime);
            if (!succeed)
            {
                return new DateTime();
            }

            return parsedDateTime;
        }

        private float ParseFloat(string floatString)
        {
            bool succeed;
            float parsedFloat;
            succeed = float.TryParse(floatString, NumberStyles.Any, CultureInfo.InvariantCulture, out parsedFloat);
            if (!succeed)
            {
                return -1;
            }

            return parsedFloat;
        }

        private int ParseHex(string hexString)
        {
            bool succeed;
            int parsedHex;
            succeed = int.TryParse(hexString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out parsedHex);
            if (!succeed)
            {
                return -1;
            }

            return parsedHex;
        }

        private int ParseInt(string intString)
        {
            bool succeed;
            int parsedInt;
            succeed = int.TryParse(intString, NumberStyles.Any, CultureInfo.InvariantCulture, out parsedInt);
            if (!succeed)
            {
                return -1;
            }

            return parsedInt;
        }

        private float ParseCoordinate(string coordinate, bool isLatitude)
        {
            bool succeed;
            int degs;
            float mins;
            int substringSize = 3;

            if (isLatitude)
            {
                substringSize = 2;
            }

            succeed = int.TryParse(coordinate.Substring(0, substringSize), NumberStyles.Any, CultureInfo.InvariantCulture, out degs);
            if (!succeed)
            {
                return -1;
            }

            succeed = float.TryParse(coordinate.Substring(substringSize), NumberStyles.Any, CultureInfo.InvariantCulture, out mins);
            if (!succeed)
            {
                return -1;
            }

            return degs + mins / 60;
        }

        #endregion

    }
}
