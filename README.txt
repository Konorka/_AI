			//-------------------------------------------------------ODA-----------------------------------------------------//
			0,	2,
			0,	10,
			35,	10,
			35,	13,
			0,	13,
			0,	22,
			35,	22,
			35,	25,
			0,	25,
			0,	34,
			35,	34,
			35,	37,
			0,	37
            //-------------------------------------------------------Vissza-----------------------------------------------------//
			20,2,
			38,2,
			38,5,
			4,5,
			4,8,
			38,8,
			38,16,
			4,16,
			4,19,
			38,19,
			38,29,
			4,29,
			4,32,
			38,32,
			38,38
			
			
			
			



1 How to create your own AI:

2 Open the solution, must have .NET 4.6.1 installed

3 Check the project BattleCity.Common / IBattleClient.cs for interface specifications

4 Add your own project into the solution (SomeNewProject, project type: Class Library)

5 If you want to implement a WPF window, then add "Framework" references to: PresentationCore, PresentationFramework, System.Xaml, WindowsBase


6 Add "Solution" reference in SomeNewProject towards: BattleCity.Common

7 Start a new class with: "public class SomeAI : IBattleClient { }"

8 Right click on the text "IBattleClient" > Resolve > add "Using BattleCity.Common;"

9 Right click on the text "IBattleClient" > Implement interface > Implement Interface

10 Obligatory parts will show up. Fill them up, see BattleCity.Common / IBattleClient.cs or BattleCity._ExampleClients / AutoClient.cs for examples


11 Add a "Solution" reference in BattleCity.ServerGUI towards your new SomeNewProject

12 Build & Run > your AI should start up, shown in the GUI list (startup project: BattleCity.ServerGUI)

13 Select the AI modules you want to try, then click on the RUN button

14 In the end, you should send us the DLL and the CS file of your AI


            if (_first)
            {
                _first = false;
                _scout(_ship1.ItemId, 0, 12);
            }
            //-----------------------------------------------------------ODA--------------------------------------------------------------//
            if (_ship1.PosX == 0 && _ship1.PosY == 12)
                _scout(_ship1.ItemId, 39,12);
            
            if (_ship1.PosX == 39 && _ship1.PosY == 12)
                _scout(_ship1.ItemId, 39, 16);

            if (_ship1.PosX == 39 && _ship1.PosY == 16)
                _scout(_ship1.ItemId, 0, 16);

            if (_ship1.PosX == 0 && _ship1.PosY == 16)
                _scout(_ship1.ItemId, 0, 20);

            if (_ship1.PosX == 0 && _ship1.PosY == 20)
                _scout(_ship1.ItemId, 39, 20);

            if (_ship1.PosX == 39 && _ship1.PosY == 20)
                _scout(_ship1.ItemId, 39, 24);
            
            if (_ship1.PosX == 39 && _ship1.PosY == 24)
                _scout(_ship1.ItemId, 0, 24);
            
            if (_ship1.PosX == 0 && _ship1.PosY == 24)
                _scout(_ship1.ItemId, 0, 28);

            if (_ship1.PosX == 0 && _ship1.PosY == 28)
                _scout(_ship1.ItemId, 39, 28);

            if (_ship1.PosX == 39 && _ship1.PosY == 28)
                _scout(_ship1.ItemId, 39, 32);

            if (_ship1.PosX == 39 && _ship1.PosY == 32)
                 _scout(_ship1.ItemId, 20 ,36);

            //-------------------------------------------------------Vissza-----------------------------------------------------//

            if (_ship1.PosX == 20 && _ship1.PosY == 36)
                _scout(_ship1.ItemId, 2, 31);
            
            if (_ship1.PosX == 2 && _ship1.PosY == 31)
                _scout(_ship1.ItemId, 36, 31);

            if (_ship1.PosX == 36 && _ship1.PosY == 31)
                _scout(_ship1.ItemId, 36, 25);

            if (_ship1.PosX == 36 && _ship1.PosY == 25)
                _scout(_ship1.ItemId, 5, 25);
            
            if (_ship1.PosX == 5 && _ship1.PosY == 25)           
                _scout(_ship1.ItemId, 5, 19);
            
            if (_ship1.PosX == 5 && _ship1.PosY == 19)
                _scout(_ship1.ItemId, 36, 19);
            
            if (_ship1.PosX == 36 && _ship1.PosY == 19)
                _scout(_ship1.ItemId, 36, 13);

            if (_ship1.PosX == 36 && _ship1.PosY == 13)
                _scout(_ship1.ItemId, 5, 13);
            
            if (_ship1.PosX == 5 && _ship1.PosY == 13)
                _first = true;
            

           
            if (_enemyPlanet == null)
                return nlb;
