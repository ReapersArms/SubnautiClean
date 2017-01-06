using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubnautiClean
{
	class SNFiles
	{
		public List<string> fileList;
		public ReaperXml reaperXml;

		public SNFiles(string[] arr, ReaperXml obj) {
			fileList = arr.ToList();
			reaperXml = obj;
		}

		// May not need this one line as a seperate method, unless adding SaveSlot and Region features makes it a more involved process
		public void GetSNFiles() {
			/* Currently Looking in SNPath, which is the path to the SaveSlot directory (so it will do nothing right now)
			 * Need to point to SaveSlot path, and also paths to 'CellsCache' and 'CompiledOctreesCache'
			 */
			var fileList = System.IO.Directory.GetFiles(reaperXml.userData.SNPath).ToList();
		}

		public void FilterFiles() {
			// For each existing file, delete file if it does not match any user specified coordinates
			foreach(string file in fileList) {
				foreach(string flag in reaperXml.userData.Coords) {
					if (!(file.Contains(flag))) {
						// Delete file or remove from list
					}
				}
			}
		}

	}
}
