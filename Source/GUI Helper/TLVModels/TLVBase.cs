using BrightIdeasSoftware;
using System.Collections.Generic;

namespace Wc3Engine
{
    public class TLVBase
    {
        public List<object> MasterList;
        public List<FolderModel> FolderMasterList;
        public List<AbilityModel> AbilityMasterList;
        public TreeListView TLV;
        
        public TLVBase(TreeListView tlv, OLVColumn olvColumn, bool dragAndDrop)
        {
            TLV = tlv;
            MasterList = new List<object>();
            FolderMasterList = new List<FolderModel>();
            AbilityMasterList = new List<AbilityModel>();

            TLV.SmallImageList.Images.Add("Folder", Properties.Resources.folderIcon);
            TLV.SmallImageList.Images.Add("DefaultIcon", Properties.Resources.BTN_unknown);

            // image getter
            olvColumn.ImageGetter = delegate (object rowObject)
            {
                if (rowObject is FolderModel)
                    return "Folder";

                else if (rowObject is AbilityModel _item)
                {
                    if (null != _item.ImageKey)
                        return _item.ImageKey;
                    else
                        return "DefaultIcon";
                }
                return "";
            };

            // when the node can be expanded
            TLV.CanExpandGetter = delegate (object x)
            {
                return x is FolderModel && ((FolderModel)x).List.Count > 0;
            };

            TLV.ChildrenGetter = delegate (object x)
            {
                if (x is FolderModel && ((FolderModel)x).List.Count > 0)
                    return ((FolderModel)x).List;
                else
                    return null;
            };

            if (dragAndDrop)
            {
                SimpleDropSink sink1 = (SimpleDropSink)TLV.DropSink;
                sink1.AcceptExternal = false;
                sink1.CanDropBetween = true;
                sink1.CanDropOnBackground = true;
            }
        }
        
        public void DeleteObject(string itemPath)
        {
            object item = FolderModel.Find(this, itemPath);

            if (null == item)
                item = AbilityModel.Find(this, itemPath);

            if (null != item)
            {
                if (item is FolderModel _folder)
                {
                    if (null != _folder.Parent)
                    {
                        _folder.Parent.List.Remove(_folder);
                        _folder.Parent.List.TrimExcess();
                    }

                    if (!itemPath.Contains(@"\"))
                    {
                        MasterList.Remove(item);
                        MasterList.TrimExcess();
                    }
                    else
                    {
                        FolderMasterList.Remove(_folder);
                        FolderMasterList.TrimExcess();
                    }
                }
                else if (item is AbilityModel _item)
                {
                    if (null != _item.Parent)
                    {
                        _item.Parent.List.Remove(_item);
                        _item.Parent.List.TrimExcess();
                    }

                    if (!itemPath.Contains(@"\"))
                    {
                        MasterList.Remove(item);
                        MasterList.TrimExcess();
                    }
                    else
                    {
                        AbilityMasterList.Remove(_item);
                        AbilityMasterList.TrimExcess();
                    }

                    TLV.SmallImageList.Images.RemoveByKey(_item.ImageKey);
                }

                TLV.SetObjects(MasterList);
            }
        }

        public void MoveObject(string sourceName, string destFolder)
        {
            bool sourceHasParent = sourceName.Contains(@"\");
            string itemName = "";
            object item = FolderModel.Find(this, sourceName);

            if (null == item)
                item = AbilityModel.Find(this, sourceName);

            if (sourceHasParent)
                itemName = sourceName.After(@"\");
            else
                itemName = sourceName;

            sourceName = sourceName.Replace(@"\" + itemName, null);
            FolderModel source = FolderModel.Find(this, sourceName);

            if (MasterList.Contains(item) || source.List.Contains(item))
            {
                if (sourceHasParent)
                {
                    source.List.Remove(item);
                    source.List.TrimExcess();
                }
                else
                {
                    MasterList.Remove(item);
                    MasterList.TrimExcess();
                    TLV.SetObjects(MasterList);
                }

                if (destFolder.EndsWith(@"\"))
                    destFolder = destFolder.Remove(destFolder.LastIndexOf(@"\"), 1);

                if ("" == destFolder)
                {
                    if (item is FolderModel folder)
                    {
                        folder.Path = itemName;
                        folder.Parent = null;
                    }
                    else if (item is AbilityModel _item)
                    {
                        _item.Path = itemName;
                        _item.Parent = null;

                        if (null != _item.ImageKey)
                        {
                            TLV.SmallImageList.Images.SetKeyName(TLV.SmallImageList.Images.IndexOfKey(_item.ImageKey), itemName);
                            _item.ImageKey = itemName;
                        }
                    }

                    MasterList.Add(item);
                    TLV.UpdateObjects(MasterList);
                }
                else
                {
                    FolderModel target;

                    if (FolderModel.Exist(this, destFolder))
                        target = FolderModel.Find(this, destFolder);
                    else
                        target = new FolderModel(this, destFolder);

                    if (item is FolderModel folder)
                    {
                        target.Add(this, item);
                        folder.Path = destFolder + @"\" + itemName;
                    }
                    else if (item is AbilityModel _item)
                    {
                        target.Add(this, item);
                        _item.Path = destFolder + @"\" + itemName;

                        if (null != _item.ImageKey)
                        {
                            TLV.SmallImageList.Images.SetKeyName(TLV.SmallImageList.Images.IndexOfKey(_item.ImageKey), itemName);
                            _item.ImageKey = _item.Path;
                        }
                    }
                }
            }
        }
    }
}
