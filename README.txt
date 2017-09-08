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