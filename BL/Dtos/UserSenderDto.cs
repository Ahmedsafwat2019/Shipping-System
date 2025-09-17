using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.Dtos;

public partial class UserSenderDto : BaseDto
{
    public Guid UserId { get; set; }

    public string SenderName { get; set; } = null!;
    [Required,DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    [Required, DataType(DataType.PhoneNumber)]
    public string Phone { get; set; } = null!;
    [Required, DataType(DataType.PostalCode)]
    public string PostalCode { get; set; }
  
    public string Contact { get; set; } = null!;
    public string OtherAddress { get; set; } = null!;
    public bool IsDefault { get; set; }
    public Guid CityId { get; set; }
    public Guid CountryId { get; set; }
    public string Address { get; set; } = null!;
}
