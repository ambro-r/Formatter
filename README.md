# Formatter
A small library to work with pre-formatted / fixed length strings. This is especially useful in the following cases:

* When submitting fixed length flat-files to legacy systems. 
* When receiving a fixed length flat-file from a legacy syste

**NOTE:** 

* Currently on generation of the fixed length file is supported, reading a fixed-length into an object is not (to do) 


### Usage:

Any models that need to be formatted need the ``[Formatted]`` attribute specified. Individual fields within the class need to be attributed with the ``[Format]`` atribute. 

#### ``[Formatted]``:

The ``[Formatted]`` attribute supports the following parameters

| Parameter | Description |
| --- | ----------- |
| **FromZero** | If the offset counts from Zero or One. If not specified, 1 is defaulted. |
| **Line** | The line number (for multi-line formatting). If none is supplied, then 1 is defaulted. |


#### ``[Format]``:

The ``[Format]`` attribute supports the following parameters

| Parameter | Description |
| --- | ----------- |
| **Offset** | The starting position. |
| **Length** | The length of the string. |
| **Fill** | What the string should be filled with. If not supplied a *space* is assumed. Note: If a string is specified, only the first character will be used. |
| **Justified** | Left or Right text justification. If not supplied LEFT is defaulted. |


#### Example (single line):

Define a *Person* object with *Formatting*:
```csharp
[Formatted]
public class Person
{
	[Format(Offset = 1, Length = 20, Justified = Justified.LEFT)]
	public string Name { get; set; }

	[Format(Offset = 21, Length = 20)]
	public string Surname { get; set; }

	[Format(Offset = 41, Length = 20, Justified = Justified.RIGHT, Fill = "0")]
	public string IdentityNumber { get; set; }

	public int Age { get; set; }			
}
```	

This will produce something like this (depending on what you've values you've assigned), notice the "AGE" is not included as it was not attributed:
```	
John                Simple              000000000A123-SIMPLE
```	
#### Example (multi-Line):

Define a *Person* object with *Formatting*:
```csharp
[Formatted]
public class Person
{
	[Format(Offset = 1, Length = 20, Justified = Justified.LEFT)]
	public string Name { get; set; }

	[Format(Offset = 21, Length = 20)]
	public string Surname { get; set; }

	[Format(Offset = 41, Length = 20, Justified = Justified.RIGHT, Fill = "0")]
	public string IdentityNumber { get; set; }

	public int Age { get; set; }	

	public ContactDetails ContactDetails { get; set; }		
}

[Formatted(FromZero = true, Line = 3)]
public class ContactDetails
{
	[Format(Offset = 0, Length = 25)]
	public string Email { get; set; }
	
	[Format(Offset = 25, Length = 15, Justified = Justified.RIGHT)]
	public string MobileNumber { get; set; }
}
```	

This will produce something like this:
```	
John                Simple              000000000A123-SIMPLE

my.email@simpleperson.com   +88-123-1234
```	