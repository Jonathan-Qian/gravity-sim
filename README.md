This is a gravity simulation made for Unity. It is not yet a full application and for now is more a project containing a few objects and scripts that can be used to simulate and visualize gravity. All masses (game objects with the MassData component) must be children of a system container object (an empty game object with the Mass System component). You can have multiple systems but they will not interact with each other (keep whatever masses you want to interact with each other in the same system). Relevant files will be found in the assets > scripts folder and include MassSystem.cs and MassData.cs for the actual simulation and DrawPath.cs and Label.cs for visual elements.
