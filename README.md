# Masking
A small library creates to add *masking* to instance variables when serializing an class to a string. This is especially useful in the following cases:

* When persisting information that may be deemed *sensitive*, but not critical for support / troubleshooting, to a log file. 
* When we more interested  in the fact that a value was received, but persisting the value will just cause bloat (i.e. if we receive an image in base64 format). 

**NOTE:** 

* *Masking* is **not** reversable!
* *Masking* is applied on serialization and DOES NOT change the value stored in the instance variable!

### Supported Masking Methods:

The following *masking methods* are supported:

#### Mask

Masks a string based on a specified regular expression pattern. 

For example, the following implementation will replace all characters between the first 3 and last 3 values of a string, hence "12345678900987654321" will become "123XXXXXXXXXXXXXX321"
```csharp
[Mask(MaskingMethod.Mask, @"(?<=.{3}).+(?=.{3})")]
public string ContactNumber { get; set; }
```		

#### Replace

Replaces a string with the specified string. 

For example, the following implemenation will replace the entire string, hence "12345678900987654321" will become "REPLACED"
```csharp
[Mask(MaskingMethod.Replace, @"REPLACED")]
public string ContactNumber { get; set; }
```		

#### Trim

Trims a string based on a specified regular expression pattern. The characters "..." will be inserted to indicate the trimmed string.

For example, the following implementation will trim all characters between the first 3 and last 3 values of a string, hence "12345678900987654321" will become "123...321"
```csharp
[Mask(MaskingMethod.Trim, @"(?<=.{3}).+(?=.{3})")]
public string ContactNumber { get; set; }
```		

### Example:

Define a *Person* object with *masks*:
```csharp
[Mask]
public class Person
{
	public string Name { get; set; }
	public string Surname { get; set; }

	[Mask(MaskingMethod.Mask, @"(?<=.{3}).+(?=.{3})")]
	public string ContactNumber { get; set; }

	[Mask(MaskingMethod.Mask, @"(?<=.{1}).+(?<=@)")]
	public string Email { get; set; }
			
	[Mask(MaskingMethod.Trim, @"(?<=.{6}).+(?=.{6})")]
	public string Address { get; set; }
			
	[Mask(MaskingMethod.Replace, @"REPLACED")]
	public string IdentityNumber { get; set; }
}
```	
Now give it some values:
```csharp
Person MaskedPerson = new Person()
{
	Name = "John",
	Surname = "Smith",
	ContactNumber = "0991231234",
	Email = "john.smith@testdomain.com",
	Address = "59 Hornbill Avenue, South Crater, Mars, 000345",
	IdentityNumber = "1234567890123"
};
```

**Direct Serializing:** 

```csharp
JsonConvert.SerializeObject(MaskedPerson);
```
outputs:
```json
{"Name":"John","Surname":"Smith","ContactNumber":"0991231234","Email":"john.smith@testdomain.com","Address":"59 Hornbill Avenue, South Crater, Mars, 000345","IdentityNumber":"1234567890123"}
```

**Serializing when applying the Mask:**
```csharp
new MaskedType<Person>(MaskedPerson).Serialize();
```
outputs:
```json
{"Name":"John","Surname":"Smith","ContactNumber":"099XXXX234","Email":"jXXXXXXXXXXtestdomain.com","Address":"59 Hor...000345","IdentityNumber":"REPLACED"}
```
