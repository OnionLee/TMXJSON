DISCLAIMER
==========
This project is in its infancy, I have a massive todo list for it including unit tests, a monogame rendering engine and ObjectLayer support. If you can be patient, I will deliver!


How to use
--------------

TMXJSON was designed to be intentionally easy to use. Born out of my own personal need to load in .TMX files saved by the Tiled map editor.

Saving Files As JSON in Tiled
---------------------------------

First, in order to save a Tiled map as JSON, you simply need to open your map in Tiled and then go to the following menu.

"File" > "Export As"

Then choose ".json" from the file types list in the save dialog window. Lastly, select your directory as you normally would and click "Save".

Using the TMXJSON libraries in code
---------------------------------------

Once you have added either a reference in your project or have added the source directly in your project, simply add this line to test:

TMXJson.TMXMap map = new TMXJson.TMXJSonReader().Load("MyLevel.json");

Within the map object, you can see a collection of Tilesets, Layers, and general map properties. The library supports both Tile and Object layer types. Object support is basic, but should be fine for simple use cases.
