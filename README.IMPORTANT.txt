* Many bugs and errors which should have been committed in task 1 or 2 were only corrected in Task 3.
* Save and Load only worked in task 2
* Sometimes the main form does not display the load and save buttons, nor the three buttons to buy from the
shop. Please rerun the program until it displays.
* For some reason items (gold and weapons) do not show on the map even though they are created. Strangely
they always show if you put a breakpoint on lines 90 and 95 in Map.cs

line 90)   Gold gold = (Gold)Create(Tile.TileType.Gold, i);
and
line 95)   Weapon weapon = (Weapon)Create(Tile.TileType.Weapon, i);


