# TeacherParser
Coming Soon!
## Usage
You may build the project as an .exe or as a .dll. If you build the project as a .dll change all of the commands from `./TeacherParser.exe --s [value] --p [value] [args]` to `./dotnet TeacherParser.dll --s [value] --p [value] [args]` 

All arguments must contain an **Subject argument** and a **Period argument**.

### Definitions:
| Semester   |      ID      | 
|:----------|:-------------:|
| Spring | 1 | 
| Summer | 2 |
| Fall | 3 |
| Winter | 4 |

- Pattern => YYYYS
- Example => 20183 (Fall 2018 semester).

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
| --sm | select many subjects | string ... string |
###### Examples:
- Example: `./TeacherParser.exe --p "20192" --s BIOL`
- Example: `./TeacherParser.exe --p "20183" --sm COM PSY`

#### Period Arguments
| Argument   |      Description      |  Pattern |
|:----------|:-------------:|------:|
| --p | select a single period | YYYYS |
| --pm | select many periods | YYYYS,YYYYS,...,YYYYS |
| --pr | select a range of periods | YYYYS-YYYYS |

###### Examples
- Example: `./TeacherParser.exe --s "CS" --pm 20162 20163 20171 20172 20173`
- Example: `./TeacherParser.exe --s "MATH" --pr 20121-20192`

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
| --minRatio | select classes w/ levels higher than min | float |
###### Examples
- Select classes below sophomore level: `./TeacherParser.exe --s "CS" --p 20153 --maxLevel 200`
- Select classes above 500 level: `./TeacherParser.exe --s "CS" --p 20153 --minLevel 450`







