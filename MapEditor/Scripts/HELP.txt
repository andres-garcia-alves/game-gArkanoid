Help for Script creation
************************

Just create a text file with 6 fields comma delimited. Each line in file is loaded into a new Brick.
Don't let empty fields. Don't let black spaces after commas.


Field formating:

Field 1: Allways 'BRICK'
Field 2: Position in X (0 to 640)
Field 3: Position in Y (0 to 480)
Field 4: Color, posibles values: 'Black', 'Blue', 'Green', 'Gray', 'Pink', 'Red', 'White', 'Yellow'
Field 5: BrickType, posible values: 1='Normal', 2='DoubleHit', 3='Idestructible'
Field 6: RewardType, posible values: 0='None', 1='WidePad', 2='FirePad', 3='SlowBall', 4='DemolitionBall', 5='DoubleBall', 6='TripleBall', 7='WinLevel'


Example:
BRICK,100,100,Red,1,0
BRICK,150,100,Green,2,0
BRICK,200,100,Red,1,0
