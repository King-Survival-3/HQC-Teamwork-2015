Refactoring documentation

Team: King-Survival-3

*Overview*

The document describes the refactoring process of the game King Survival. Main purpose of the project is to provide high quality code following the best practices of the programming practices introduced in the course "High-Quality Programming Code".
On the basis of the source code provided the team has made refactoring and improvements of the code while following the best practices. As a result the obtained source code is readable, maintainable and testable. All the refactoring steps will be described and will be explained their role for the final result.
Refactoring process

*Redesigned the code structure*

1. The game logic was placed in a seperate project. Its clients (console/web/wpf) can reference the game logic and reuse it.

2. Maming the files with appropriate names according to their function for the game logic. For example:
	a.	Board for the game board
	b.	GameEngine for the game engine
3. Extracting all the constants (strings) in a public class named GlobalConstants in a Common folder.

4. Renaming for example
	a. ConsoleApplication1 - the entry point was placed in a new Project where a face class was introduced.
	b. Proverka2 was renamed to WinningConditions in the GameEngine class
	c. Structure named Move was introduced instead of the strings KUL KUR KDR KDL; Moving is now made as in chess "a1-a3"
	

*Code reformatting*

1.	Moved the using directives inside the namespace in all files.
2.	Added line spaces when needed – for example after all block enclosed by curly braces.
3.	Removed extra empty lines where needed – (for example in the file ConsoleApplication1.cs).
4.	Rename class members according to the appropriate code convention using intuitive names. For example - ConsoleRenderer is used for printing to console; ConsoleInputProvided is used for user input (read the moves) via console;

*Design patterns*

Implemented design patterns:
•	Structural patterns – 
	- Facade - used to hide big logic for the starting of the game. It provides all the necessary classes like IRenderer, IInputProvider, IChessEngine, IGameInitializationStrategy
	- Repository pattern (sort of facade?) - it is used in the WEB client.
	- Bridge pattern - it is implemened in the Console client. Based on provided IFormatter implementation otput strings are formated in different ways. With fancy formating (-= {text} =- ) or text only;

•	Creational patterns 
	– Simple factory - the chess facade uses InitializationGameProvider and based on selected game type (chess or king survival) creates GameInitialization, Movement strategy and GameEngine;
		
•	Behavioural patterns:
	- Strategy - used for game initialization. User can select to play "King Survival" or "Chess"

*Unit tests*

We designed and implemented unit tests covering the entire project functionality. To ensure the project works correctly according to the requirements and behaves correctly in all possible use cases. In order to make the code testable the code was redesigned but not the program logic. 
Tests cover normal expected behavior (correct data) and possible expected failures (incorrect data). Special attention was put to the border cases. The code coverage of the unit tests is xx%.

*Principles followed*

We tried to follow the idea of strong cohesion and loose coupling.  Mainly we followed the SOLID and DRY principles.
SOLID – ever class maintains only its own functionality. For instance ConsoleRendered prints info only when it is requested. 

