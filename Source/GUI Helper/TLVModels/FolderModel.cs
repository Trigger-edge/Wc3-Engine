using System.Collections.Generic;

namespace Wc3Engine
{
    public class FolderModel
    {
        public string Name { get; set; }
        public string Suffix { get; set; }
        public FolderModel Parent { get; set; }
        public string Path { get; set; }
        public List<object> List;

        internal FolderModel(TLVBase tlvModel, string folderPath)
        {
            if (folderPath.Contains(@"\"))
                Name = folderPath.After(@"\");

            else
            {
                Name = folderPath;
                tlvModel.MasterList.Add(this);
                tlvModel.TLV.UpdateObjects(tlvModel.MasterList);
            }

            Path = folderPath;
            List = new List<object>();
            tlvModel.FolderMasterList.Add(this);

            if (folderPath.Before(@"\" + Name) != "")
            {
                string parentFolder = folderPath.Before(@"\" + Name);

                if (!Exist(tlvModel, parentFolder))
                    new FolderModel(tlvModel, parentFolder).Add(tlvModel, this);
                else
                    Find(tlvModel, parentFolder).Add(tlvModel, this);
            }
        }

        internal static FolderModel Find(TLVBase tlvModel, string path)
        {
            return tlvModel.FolderMasterList.Find(x => x.Path == path);
        }

        internal static bool Exist(TLVBase tlvModel, string path)
        {
            return tlvModel.FolderMasterList.Exists(x => x.Path == path);
        }

        internal static void Add(TLVBase tlvModel, string path, object item)
        {
            Find(tlvModel, path).Add(tlvModel, item);
        }

        internal void Add(TLVBase tlvModel, object item)
        {
            if (!List.Contains(item))
            {
                if (item is FolderModel folder)
                    folder.Parent = this;

                else if (item is AbilityModel _item)
                    _item.Parent = this;

                List.Add(item);
                tlvModel.TLV.UpdateObjects(tlvModel.MasterList);
            }
            else
            {
                if (item is FolderModel folder)
                    Wc3Engine.DebugMsg("[LTVAbilityModel] error: Warning this object allready exist in folder: Folder: " + Name + " Object: " + folder.Name);

                else if (item is AbilityModel _item)
                    Wc3Engine.DebugMsg("[LTVAbilityModel] error: Warning this object allready exist in folder: Folder: " + Name + " Object: " + _item.Name);
            }
        }
        /*
        internal void InsertAbove(TLVBase tlvModel, int index, object item)
        {
            if (!List.Contains(item))
            {
                if (item is FolderModel folder)
                    folder.Parent = this;

                else if (item is AbilityModel _item)
                    _item.Parent = this;

                List.Insert(index, item);
                tlvModel.TLV.UpdateObjects(tlvModel.MasterList);
            }
            else
            {
                if (item is FolderModel folder)
                    Wc3Engine.DebugMsg("[LTVAbilityModel] error: Warning this object allready exist in folder: Folder: " + Name + " Object: " + folder.Name);

                else if (item is AbilityModel _item)
                    Wc3Engine.DebugMsg("[LTVAbilityModel] error: Warning this object allready exist in folder: Folder: " + Name + " Object: " + _item.Name);
            }
        }*/
    }
}
