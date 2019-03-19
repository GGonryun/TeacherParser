# TeacherParser
Track historical data about classes, find classes or sort through classes with much more power! Currently only implemented for **San Diego State University**. It has been used to help me determine some simple statistics about my school, classes, and professors.

## Building

You may build the project as an .exe or as a .dll. If you build the project as a .dll change all of the commands from `./TeacherParser.exe --s [value] --p [value] [args]` to `./dotnet TeacherParser.dll --s [value] --p [value] [args]` 

[I recommended you use Warp when building the project to create a stand alone executable.](https://github.com/dgiagio/warp#windows-1)

[You can use the pre-built executable by clicking here!](./Exe/)
###### Example:
- Build Release: `dotnet publish -c Release -r win10-x64`
- Place warp-packer.exe in the base directory (where you git clone to).
- Build Executbale: `.\warp-packer --arch windows-x64 --input_dir ClassStatistics/bin/Release/netcoreapp2.1/win10-x64/publish --exec ClassStatistics.exe --output ClassStatistics.exe`
- The new executable will be in the same location as **warp-packer.exe**.

## Usage

All arguments must contain an **Subject argument** and a **Period argument**.

Arguments that contain spaces must be held together using quotes, see examples below.

### Definitions:
- Course Subject Codes can be found here: `https://sunspot.sdsu.edu/schedule/search`
- Course Code Pattern => XYZ-###
- Example => COM-101 | CS-570 | MATH-251 | "A D-200"
- Period Code Pattern => YYYYS
- Example => 20183 (Fall 2018 semester).
- Hour Code Pattern => HHMM
- Example => 1300
- Hour Range Code Pattern => HHMM-HHMM
- Example => 0900-1700

| Semester   |      ID      | 
|:----------|:-------------:|
| Winter | 1 |
| Spring | 2 | 
| Summer | 3 |
| Fall | 4 |

| Class   |      Level      | 
|:----------|:-------------:|
| Freshman | 100 | 
| Sophomore | 200 |
| Junior | 300 |
| Senior | 400 |
| Graduate | 600 |


#### Subject Arguments
| Argument | Description | Pattern|
|:----------|:-------------:|------:|
| --s | select a single subject | string |
| --sm | (multiargument) select many subjects | string ... string |
###### Examples:
- Display all BIOL classes for the Spring 2019 semester: `./TeacherParser.exe --p "20192" --s BIOL`
- Display all COM and BIOL classes for Fall 2018 `./TeacherParser.exe --p "20183" --sm COM PSY`

#### Period Arguments
| Argument   |      Description      |  Pattern |
|:----------|:-------------:|------:|
| --p | select a single period | YYYYS |
| --pm | (multiargument) select many periods | YYYYS YYYYS ... YYYYS |
| --pr | select a range of periods | YYYYS-YYYYS |

###### Examples
- Display all CS classes for Spring 2016, Summer 2016, Winter 2017, Spring 2017, Summer 2017: `./TeacherParser.exe --s "CS" --pm 20162 20163 20171 20172 20173`
- Display all MATH classes from Winter 2012 to Spring 2019: `./TeacherParser.exe --s "MATH" --pr 20121-20192`

#### Fill Ratio Arguments (Inclusive)

| Argument   |      Description      |  Pattern |
|:----------|:-------------:|------:|
| --maxRatio | select classes smaller than max ratio | float |
| --minRatio | select classes larger than min ratio | float |
###### Examples
- Select classes that are not full: `./TeacherParser.exe --s "CS" --p 20153 --maxRatio 1.0` 
- Select classes that are at least 25% full: `./TeacherParser.exe --s "CS" --p 20153 --minLevel 0.25`

#### Level Arguments (Inclusive)

| Argument   |      Description      |  Pattern |
|:----------|:-------------:|------:|
| --maxLevel | select classes w/ levels lower than max | float |
| --minLevel | select classes w/ levels higher than min | float |
###### Examples
- Select classes below sophomore level: `./TeacherParser.exe --s "CS" --p 20153 --maxLevel 200`
- Select classes above 500 level: `./TeacherParser.exe --s "CS" --p 20153 --minLevel 450`

#### Course Arguments
| Argument | Description | Pattern |
|:---------|:----------------:|----------:|
|--course-num| selects all classes that match course number | int |
|--course-sub| selects all classes that match course subject | string |

###### Examples
- Select all 101 classes: `./TeacherParser.exe --s CS --p 20153 --course-num 101`
- Select all CS classes: `./TeacherParser.exe --sm "A D" CS BIOL --p 20173 --course-sub CS`

#### Ignore Class Arguments
| Argument | Description | Pattern |
|:---------|:----------------:|----------:|
| --ignore | (multiargument) filters out the specified classes | class-code ... class-code |
###### Examples
- Select all CS classes that are not 101, 237, and 490: `./TeacherParser.exe --s CS --p 20173 --ignore CS-101 CS-237 CS-490`

#### Instructor Arguments
| Argument | Description | Pattern |
|:---------|:----------------:|----------:|
| --prof | selects classes that match a specific professor. | string |
| --notprof | selects classes that do not contain the specified professor. | string |
| --notprofs | (multiargument) selects classes that are not taught by any of the professors listed. | string ... string |

###### Examples
- Selects all classes that are not taught by the specified professor: `./TeacherParser.exe --s CS --p 20184 --notprof "G. LEONARD"`
- Selects all classes that are taught by the specified professor: `./TeacherParser.exe --s CS --p 20184 --prof "J. CARROLL"`
- Selects all classes that are not taught by the specified professors: `./TeacherParser.exe --s CS --p 20192 --notprofs "G. LEONARD" "W. WANG" "K. LEVI"`

#### Time Arguments (Inclusive)
| Argument | Description | Pattern |
|:---------|:----------------:|----------:|
| --outside | selects classes are outside the specified time range. | "HHMM-HHMM" |
| --within | selects classes that do not contain the specified professor. | "HHMM" "HHMM-HHMM" |
| --start-after | selects classes that start after a specified time. | "HHMM" |
| --end-after | selects classes that end after a specified time. | "HHMM" |
| --start-before | selects classes that start before a specified time. | "HHMM" |
| --end-before | selects classes that end before a specified time. | "HHMM" |

###### Examples
- Selects all classes that are within 30 minutes of another time range: `./TeacherParser.exe --s PSY --p 20134 --within 0030 1030-1230`
- Selects all classes that are outside the time range. `./TeacherParser.exe --s PSY --p 20134 --outside 1030-1230`
- Selects all classes that start after 9 AM. `./TeacherParser.exe --s PSY --p 20163 --start-after 0900`
- Selects all classes that end before 6 PM. `./TeacherParser.exe --s PSY --p 20142 --end-before 1800`

#### Teaching Format Arguments
| Argument | Description | Pattern |
|:---------|:----------------:|:----------:|
| --format | selects all classes that match a specified format. |  \[Activity, Discussion, Laboratory, Lecture, Nontraditional, ROTC, Seminar, Supervised, None.] |
| --notformat | selects all classes that don't match a specified format. |  \[Activity, Discussion, Laboratory, Lecture, Nontraditional, ROTC, Seminar, Supervised, None.] |

- Select all classes that follow a Lecture format: `./TeacherParser.exe --s CHEM --p 20184 --format lecture`.

#### Display Arguments
| Argument | Description | Pattern|
|:----------|:-------------:|------:|
| --q or --quiet | do not display any classes to the screen. | no args |
| --v or --verbose | display classes with extra verbosity.  | no args |
| --popularity | display all professors and their popularity ranking  | no args |

###### Examples:
- This will not display any of the classes but prints out popularity rankings: `./TeacherParser.exe --p "20192" --s MKT --q --popularity`
