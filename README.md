How to useNew Page Edit Page Page History
TMXJSON was designed to be intentionally easy to use. Born out of my own personal need to load in .TMX files saved by the Tiled map editor.

Saving Files As JSON in Tiled
First, in order to save a Tiled map as JSON, you simply need to open your map in Tiled and then go to the following menu.

"File" > "Export As"

Then choose ".json" from the file types list in the save dialog window. Lastly, select your directory as you normally would and click "Save".

Using the TMXJSON libraries in code
Once you have added either a reference in your project or have added the source directly in your project, simply add this line to test:

TMXJson.TMXMap map = new TMXJson.TMXJSonReader().Load("MyLevel.json");

Within the map object, you can see a collection of Tilesets, Layers, and general map properties. As of this writing, the library only support "TileLayer" types. I hope to add "ObjectLayer" types very soon.
