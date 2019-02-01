using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ted.Model.Auth;

namespace Ted.Model.DTO
{
    public class UserListItemDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public UserListItemDTO()
        {
        }

        public UserListItemDTO(User user, string role)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;
            Role = role;
        }

        public static IEnumerable<UserListItemDTO> ToDeviceDTOList(List<Tuple<User, string>> usersRoles)
        {
            return usersRoles.Select(x => new UserListItemDTO(x.Item1,x.Item2));
        }
    }
}