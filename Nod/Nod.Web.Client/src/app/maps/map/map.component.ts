import { Component, OnInit, SimpleChanges, Input } from '@angular/core';
import { DatePipe } from '@angular/common'

import { tileLayer, latLng, marker, icon, geoJSON, map, } from 'leaflet';

import { Gps } from '../../device/device.model';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {

  @Input() gpses: Gps[];

  path = [];

  markerIcon;
  options;
  layersControl;
  layers;
  polyline;

  zoom: number = 6;
  center = latLng(37.974848, 23.734001);


  constructor(public datepipe: DatePipe) { }

  ngOnInit() {
    this.renderMap();
  }

  ngOnChanges(changes: SimpleChanges) {
    // only run when property "data" changed
    if (
      changes['gpses'] &&
      this.gpses != undefined &&
      this.gpses.length != 0 &&
      this.markerIcon != undefined
    ) {
      this.path = this.gpses.filter(x => x.Lattitude != -1 && x.Longtitude != -1);
      this.zoom = 17;
      this.center = latLng(this.gpses[0].Lattitude, this.gpses[0].Longtitude);
      this.generatePath(this.path);
    }
  }

  renderMap() {
    this.markerIcon = icon({
      iconUrl: 'assets/marker-icon-2x.png',
      shadowUrl: 'assets/marker-shadow.png',

      iconSize: [25, 41], // size of the icon
      shadowSize: [50, 64], // size of the shadow
      //iconAnchor: [22, 94], // point of the icon which will correspond to marker's location
      //shadowAnchor: [4, 62],  // the same for the shadow
      //popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
    });

    this.options = {
      layers: [
        tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 18, attribution: '...' })
      ],
      zoom: this.zoom,
      center: this.center
    };

    this.layersControl = {
      baseLayers: {
        'Open Street Map': tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 18, attribution: '...' }),
        'Open Cycle Map': tileLayer('http://{s}.tile.opencyclemap.org/{z}/{x}/{y}.png', { maxZoom: 18, attribution: '...' })
      }
    }
  }

  generatePath(path: Gps[]) {
    this.layers = [marker([this.path[0].Lattitude, this.path[0].Longtitude], { icon: this.markerIcon })];
    for (let index = 0; index < path.length - 1; index++) {
      var cord1 = latLng(this.path[index].Lattitude, this.path[index].Longtitude);
      var cord2 = latLng(this.path[index + 1].Lattitude, this.path[index + 1].Longtitude);
      if (cord1.distanceTo(latLng(cord2)) < 3) {
        continue;
      }
      var line = geoJSON(
        ({
          type: 'LineString',
          coordinates: [
            [cord1.lng, cord1.lat],
            [cord2.lng, cord2.lat]
          ],
        }) as any,
        { style: () => ({ color: 'blue', weight: 5 }) });
      line.bindPopup(
        "Date - Time: " + this.datepipe.transform(path[index].DateTime, 'dd/MM/yyyy - HH:mm:ss') + "<br>" +
        "Speed: " + path[index].CurrentSpeed + "\n"
      );
      line.on('mouseover', function (e) {
        this.openPopup();
      });
      line.on('mouseout', function (e) {
        this.closePopup();
      });
      this.layers.push(line)
    }
  }
}