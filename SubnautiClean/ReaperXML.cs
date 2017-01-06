using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SubnautiClean
{
    public class ReaperXml
    {
		// Define schema for subnautica Xml

		// Create and validate SubXML's using XmlDocument

		// Read data from SubXML's

		// Write data to SubXML's

		private XmlDocument doc;
		public UserXmlData userData;

		public ReaperXml() {
			// Initilize ReaperXml
			//doc = new XmlDocument();
			//CreateSubXml();
			userData = new UserXmlData();
			userData.Coords = new List<string>();
		}

		public struct UserXmlData
		{
			// TODO: Add support for multiple save slots
			// TODO: Add support for selecting regions of a save slot for selective resets?
			/* Both of these will require making the Slot and Region section a list
			 * and then somehow making the Coords an array of lists (or list of lists?) associated with each region/Slot
			 * Either that, or nesting them within eachother. I dont know, we'll see. Complicated. For now just get it working.
			 */
			public string SNPath;
			public string Slot;
			public string Region;
			public List<string> Coords;

			public UserXmlData(string path, string slot, string region, string co) {
				SNPath = path;
				Slot = slot;
				Region = region;
				Coords = new List<string>() {co};
			}
		}

        public void CreateSubXml() {
			// TODO: Create the default XML document using the Writer instead, no need to load and save with the XmlDocument
            doc.PreserveWhitespace = true;
            try {
                doc.Load("config.xml");
            }
            catch (System.IO.FileNotFoundException) {
                doc.LoadXml("<?xml version=\"1.0\"?> \n" +
                    "<SNAppData path=\"filePathText\"> \n" +
					"	<SaveSlot slot=\"slotXXXX\"> \n" +
					"		<Region tag=\"UserRegion\"> \n" +
					"			<Coordinates> xx-xx-xx </Coordinates>\n" +
					"		</Region> \n" +
					"	</SaveSlot> \n" +
					"</SNAppData> \n");
            }
			doc.Save("config.xml");
        }

		public void ReadSubXml() {
			XmlTextReader reader = new XmlTextReader("config.xml");

			while (reader.Read()) {
				if (reader.NodeType == XmlNodeType.Element) {
					switch (reader.Name) {
						case "SNAppData":
							reader.MoveToAttribute("path");
							userData.SNPath = reader.Value;
							Console.WriteLine("Path: " + userData.SNPath);
							break;
						case "SaveSlot":
							reader.MoveToAttribute("slot");
							userData.Slot = reader.Value;
							Console.WriteLine("Slot: " + userData.Slot);
							break;
						case "Region":
							reader.MoveToAttribute("tag");
							userData.Region = reader.Value;
							Console.WriteLine("Region: " + userData.Region); // currently overwrites the previous region if any
							break;
						case "Coordinates":
							reader.Read(); // Read to the text node of Coordinates element
							userData.Coords.Add(reader.Value);
							break;
					}
				}

				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "SNAppData") {
					foreach (string co in userData.Coords) {
						Console.WriteLine("Coords: " + co);
					}
					Console.WriteLine("End of File!");
				}
			}

		}
    }
}
