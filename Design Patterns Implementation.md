# Implemented design patterns:

## Behavioural patterns:
- Strategy - used for game initialization. Can be 
	+ King Survival
	+ Chess

## Structural patterns:
- Facade - used to hide the starting the game with:
	+ IRenderer;
	+ IInputProvider;
	+ IChessEngine
	+ IGameInitializationStrategy
	
- Repository pattern (sort of facade?)

- Bridge pattern
	+ used for Console client
	+ based on provided IFormatter implementation otput strings are formated in different ways;
	
## Creational patterns:
- Simple factory

### Suggestions:
- IClonable for Pawn (not implemented yet!)
- Memento - for moves (so player can reverse move) (not implemented yet!)
- Builder - color, enum (not implemented yet!)
- Flyweight for board (white/black)