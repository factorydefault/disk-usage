# Disk Usage :cd: :floppy_disk: :computer: 

> I have multiple disks and network shares across multiple computers and I need to keep track of how much disk space i'm using (without mapping every drive), how do i do that?

disk-usage is a desktop utility to keep track of how full your :cd:'s are getting. Supports Windows with either local drives and network shares. For downloads please see the [releases page](https://github.com/factorydefault/disk-usage/releases).

![Disk Usage Screenshot](/screenshots/mainscreen.png?raw=true "Disk Usage Popup Window")

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. 

### Prerequisites

* Visual Studio (only tested on Visual Studio 2015)

### Installing

* Open solution using Visual Studio
* Build `Debug` or `Release`
* Executable can be found in `disk-usage\disk-usage-ui\bin\[Debug/Release]\`

The Disk usage utility lives in your taskbar, click it to see the status of drives, or right click to add additional drives or quit.

## Running the tests

Tests have not yet been developed for this project. [TODO]

## Built With

* [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) - JSON framework for .NET
* Fody/[Costura](https://github.com/Fody/Costura) - To embed everything into a single .EXE
* [ByteSize](https://github.com/omar/ByteSize) - Directory size conversions; bytes to GB etc.

## Versioning

The intent is to use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/factorydefault/disk-usage/tags). 

## Authors

* **Alex H** - *Initial work* - [factorydefault](https://github.com/factorydefault)

See also the list of [contributors](https://github.com/factorydefault/disk-usage/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
