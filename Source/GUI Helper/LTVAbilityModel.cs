using BrightIdeasSoftware;
using System.Collections.Generic;
using System.Drawing;

namespace Wc3Engine
{
    public class TLVModel
    {
        public List<object> MasterList;
        public List<Folder> FolderMasterList;
        public List<Item> ItemMasterList;
        private TreeListView TLV;

        #region Folder Structure
        public class Folder
        {
            public string Name { get; set; }
            public string Suffix { get; set; }
            public Folder Parent { get; set; }
            public string Path { get; set; }
            public List<object> List;

            internal Folder(TLVModel tlvModel, string folderPath)
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

                    if (!tlvModel.FolderExist(parentFolder))
                        new Folder(tlvModel, parentFolder).AddItemToFolder(tlvModel, this);
                    else
                        Find(tlvModel, parentFolder).AddItemToFolder(tlvModel, this);
                }
            }

            internal static Folder Find(TLVModel tlvModel, string path)
            {
                return tlvModel.FolderMasterList.Find(x => x.Path == path);
            }

            internal void AddItemToFolder(TLVModel tlvModel, object item)
            {
                if (!List.Contains(item))
                {
                    if (item is Folder folder)
                        folder.Parent = this;

                    else if (item is Item _item)
                        _item.Parent = this;

                    List.Add(item);
                    tlvModel.TLV.UpdateObjects(tlvModel.MasterList);
                }
                else
                {
                    if (item is Folder folder)
                        Wc3Engine.DebugMsg("[LTVAbilityModel] error: Warning this object allready exist in folder: Folder: " + Name + " Object: " + folder.Name);

                    else if (item is Item _item)
                        Wc3Engine.DebugMsg("[LTVAbilityModel] error: Warning this object allready exist in folder: Folder: " + Name + " Object: " + _item.Name);
                }
            }

            internal void InsertAbove(TLVModel tlvModel, int index, object item)
            {
                if (!List.Contains(item))
                {
                    if (item is Folder folder)
                        folder.Parent = this;

                    else if (item is Item _item)
                        _item.Parent = this;

                    List.Insert(index, item);
                    tlvModel.TLV.UpdateObjects(tlvModel.MasterList);
                }
                else
                {
                    if (item is Folder folder)
                        Wc3Engine.DebugMsg("[LTVAbilityModel] error: Warning this object allready exist in folder: Folder: " + Name + " Object: " + folder.Name);

                    else if (item is Item _item)
                        Wc3Engine.DebugMsg("[LTVAbilityModel] error: Warning this object allready exist in folder: Folder: " + Name + " Object: " + _item.Name);
                }
            }
        }
        #endregion

        #region Item Structure
        public class Item
        {
            public string Name { get; set; }
            public string Suffix { get; set; }
            public int Id { get; set; }
            public Folder Parent { get; set; }
            public string Path { get; set; }
            public string ImageKey { get; set; }
            public Ability Ability { get; set; }

            internal Item(TLVModel tlvModel, string itemPath, Image image, int abilityId)
            {
                if (itemPath.Contains(@"\"))
                {
                    Name = itemPath.After(@"\");

                    if (!tlvModel.FolderExist(itemPath.Replace(@"\" + Name, null)))
                        new Folder(tlvModel, itemPath.Replace(@"\" + Name, null)).AddItemToFolder(tlvModel, this);
                    else
                        Folder.Find(tlvModel, itemPath.Replace(@"\" + Name, null)).AddItemToFolder(tlvModel, this);
                }
                else
                {
                    Name = itemPath;
                    tlvModel.MasterList.Add(this);
                    tlvModel.TLV.UpdateObjects(tlvModel.MasterList);
                }

                Path = itemPath;
                tlvModel.ItemMasterList.Add(this);

                if (0 == abilityId)
                {
                    Id = ObjectId.GenerateAbilityId();
                    Ability = new Ability(Name, Suffix, Id, this);
                }
                else
                {
                    Id = abilityId;
                    ObjectId.AddAbilityId(abilityId);
                }

                Name = "(" + Id.ToObjectId() + ") " + Name;

                if (null != image && !tlvModel.TLV.SmallImageList.Images.ContainsKey(itemPath))
                {
                    ImageKey = itemPath;
                    tlvModel.TLV.SmallImageList.Images.Add(ImageKey, image);
                }
            }

            internal static Item Find(TLVModel tlvModel, string path)
            {
                return tlvModel.ItemMasterList.Find(x => x.Path == path);
            }
        }
        #endregion

        public TLVModel(TreeListView tlv, OLVColumn olvColumn, bool dragAndDrop)
        {
            TLV = tlv;
            MasterList = new List<object>();
            FolderMasterList = new List<Folder>();
            ItemMasterList = new List<Item>();

            TLV.SmallImageList.Images.Add("Folder", Properties.Resources.folderIcon);
            TLV.SmallImageList.Images.Add("DefaultIcon", Properties.Resources.BTN_unknown);

            // image getter
            olvColumn.ImageGetter = delegate (object rowObject)
            {
                if (rowObject is Folder)
                    return "Folder";

                else if (rowObject is Item _item)
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
                return x is Folder && ((Folder)x).List.Count > 0;
            };

            TLV.ChildrenGetter = delegate (object x)
            {
                if (x is Folder && ((Folder)x).List.Count > 0)
                    return ((Folder)x).List;
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

        public bool CreateFolder(string folderPath)
        {
            return !FolderMasterList.Exists(x => x.Path == folderPath) && new Folder(this, folderPath) != null;
        }

        public bool CreateItem(string itemPath)
        {
            return !ItemMasterList.Exists(x => x.Path == itemPath) && new Item(this, itemPath, null, 0) != null;
        }

        public bool CreateItem(string itemPath, int id)
        {
            return !ItemMasterList.Exists(x => x.Path == itemPath) && new Item(this, itemPath, null, id) != null;
        }

        public bool CreateItem(string itemPath, Image image, int id)
        {
            return !ItemMasterList.Exists(x => x.Path == itemPath) && new Item(this, itemPath, image, id) != null;
        }

        public void ChangeItemIcon(string itemPath, Image image)
        {
            if (null == image)
                Item.Find(this, itemPath).ImageKey = null;
            else

            {
                TLV.SmallImageList.Images.RemoveByKey(Item.Find(this, itemPath).ImageKey);
                TLV.SmallImageList.Images.Add(itemPath, image);
            }
        }

        public bool FolderExist(string path)
        {
            return FolderMasterList.Exists(x => x.Path == path);
        }

        public bool ItemExist(string itemPath)
        {
            return ItemMasterList.Exists(x => x.Path == itemPath);
        }

        public void Add(string path, object item)
        {
            Folder.Find(this, path).AddItemToFolder(this, item);
        }

        public void Delete(string itemPath)
        {
            object item = Folder.Find(this, itemPath);
            if (null == item)
                item = Item.Find(this, itemPath);

            if (null != item)
            {
                Wc3Engine.DebugMsg("dsdsd");
                if (item is Folder _folder)
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
                else if (item is Item _item)
                {
                    if (null != _item.Parent)
                    {
                        _item.Parent.List.Remove(_item);
                        _item.Parent.List.TrimExcess();
                    }

                    if (!itemPath.Contains(@"\"))
                    {
                        Wc3Engine.DebugMsg("dsdsd");

                        MasterList.Remove(item);
                        MasterList.TrimExcess();
                    }
                    else
                    {
                        ItemMasterList.Remove(_item);
                        ItemMasterList.TrimExcess();
                    }

                    TLV.SmallImageList.Images.RemoveByKey(_item.ImageKey);
                }

                TLV.SetObjects(MasterList);
            }
        }

        public void Move(string sourceName, string destFolder)
        {
            bool sourceHasParent = sourceName.Contains(@"\");
            string itemName = "";
            object item = Folder.Find(this, sourceName);

            if (null == item)
                item = Item.Find(this, sourceName);

            if (sourceHasParent)
                itemName = sourceName.After(@"\");
            else
                itemName = sourceName;

            sourceName = sourceName.Replace(@"\" + itemName, null);
            Folder source = Folder.Find(this, sourceName);

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
                    if (item is Folder folder)
                    {
                        folder.Path = itemName;
                        folder.Parent = null;
                    }
                    else if (item is Item _item)
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
                    Folder target;

                    if (FolderExist(destFolder))
                        target = Folder.Find(this, destFolder);
                    else
                        target = new Folder(this, destFolder);

                    if (item is Folder folder)
                    {
                        target.AddItemToFolder(this, item);
                        folder.Path = destFolder + @"\" + itemName;
                    }
                    else if (item is Item _item)
                    {
                        target.AddItemToFolder(this, item);
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

        /*
        public void MoveAbove(string sourceName, string targetPath)
        {
            bool sourceHasParent = sourceName.Contains(@"\");
            string itemName = "";
            object item = Folder.Find(this, sourceName);

            if (null == item)
                item = Item.Find(this, sourceName);

            if (sourceHasParent)
                itemName = sourceName.After(@"\");
            else
                itemName = sourceName;

            sourceName = sourceName.Replace(@"\" + itemName, null);
            Folder source = Folder.Find(this, sourceName);

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

                if (targetPath.EndsWith(@"\"))
                    targetPath = targetPath.Remove(targetPath.LastIndexOf(@"\"), 1);

                object targetItem = MasterList.Find(y => ((Folder)y).Path == targetPath);
                if (null == targetItem)
                    targetItem = MasterList.Find(y => ((Item)y).Path == targetPath);

                if ("" == targetPath)
                {
                    if (item is Folder folder)
                    {
                        folder.Path = itemName;
                        folder.Parent = null;
                    }
                    else if (item is Item _item)
                    {
                        _item.Path = itemName;
                        _item.Parent = null;

                        if (null != _item.ImageKey)
                        {
                            TLV.SmallImageList.Images.SetKeyName(TLV.SmallImageList.Images.IndexOfKey(_item.ImageKey), itemName);
                            _item.ImageKey = itemName;
                        }
                    }

                    MasterList.Insert(MasterList.IndexOf(targetItem), item);
                    TLV.UpdateObjects(MasterList);
                }
                else
                {
                    if (item is Folder folder)
                    {
                        if (targetItem is Folder)
                            ((Folder)targetItem).Parent.InsertAbove(this, ((Folder)targetItem).Parent.List.IndexOf(targetItem), item);
                        else if (item is Item)
                            ((Item)targetItem).Parent.InsertAbove(this, ((Item)targetItem).Parent.List.IndexOf(targetItem), item);

                        folder.Path = targetPath + @"\" + itemName;
                    }
                    else if (item is Item _item)
                    {
                        //target.InsertAbove(this, item);
                        if (targetItem is Folder)
                            ((Folder)targetItem).Parent.InsertAbove(this, ((Folder)targetItem).Parent.List.IndexOf(targetItem), item);
                        else if (item is Item)
                            ((Item)targetItem).Parent.InsertAbove(this, ((Item)targetItem).Parent.List.IndexOf(targetItem), item);

                        _item.Path = targetPath + @"\" + itemName;

                        if (null != _item.ImageKey)
                        {
                            TLV.SmallImageList.Images.SetKeyName(TLV.SmallImageList.Images.IndexOfKey(_item.ImageKey), itemName);
                            _item.ImageKey = _item.Path;
                        }
                    }
                }
            }
        }*/
    }
}
