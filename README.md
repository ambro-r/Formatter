# Formatter
A small library to work with pre-formatted / fixed length strings. This is especially useful in the following cases:

* When submitting fixed length flat-file(s) to a legacy system(s). 
* When receiving fixed length flat-file(s) from a legacy system(s).

**TO DO / NOTE:** 

* Limited support for reading of fixed length strings (no support for lists).
* Need to add support for "skipped" lines. (i.e ``[Format(Line = 3)]``, without ``[Format(Line = 2)]``).


### Usage:

Any models that need to be formatted need the ``[Formatted]`` attribute specified. Individual fields within the class need to be attributed with the ``[Format]`` atribute. 

#### ``[Formatted]``:

The ``[Formatted]`` attribute supports the following parameters

| Parameter | Description |
| --- | ----------- |
| **FromZero** | If the offset counts from Zero or One. If not specified, 1 is defaulted. |


#### ``[Format]``:

The ``[Format]`` attribute supports the following parameters

| Parameter | Description |
| --- | ----------- |
| **Offset** | The starting position. |
| **Length** | The length of the string. |
| **Fill** | What the string should be filled with. If not supplied a *space* is assumed. Note: If a string is specified, only the first character will be used. |
| **Justified** | Left or Right text justification. If not supplied LEFT is defaulted. |
| **Case** | Change the CASE (UPPER or lower). If not supplied case will not be changed. |
| **Line** | The line number (for multi-line formatting). If none is supplied, then 1 is defaulted. |


#### Example (single line):

Define a *Person* object with *Formatting*:
```csharp
[Formatted]
public class Person
{
	[Format(Offset = 1, Length = 20, Justified = Justified.LEFT, Case = Case.UPPER)]
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

	[Format(Line = 2)]
	public ContactDetails ContactDetails { get; set; }		
}

[Formatted(FromZero = true)]
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