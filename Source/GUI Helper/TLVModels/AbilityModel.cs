using System.Drawing;

namespace Wc3Engine
{
    public class AbilityModel
    {
        public string Name { get; set; }
        public string Suffix { get; set; }
        public int Id { get; set; }
        public FolderModel Parent { get; set; }
        public string Path { get; set; }
        public string ImageKey { get; set; }

        internal AbilityModel(TLVBase tlvModel, string itemPath, Image image, int abilityId)
        {
            if (itemPath.Contains(@"\"))
            {
                Name = itemPath.After(@"\");

                if (!FolderModel.Exist(tlvModel, itemPath.Replace(@"\" + Name, null)))
                    new FolderModel(tlvModel, itemPath.Replace(@"\" + Name, null)).Add(tlvModel, this);
                else
                    FolderModel.Find(tlvModel, itemPath.Replace(@"\" + Name, null)).Add(tlvModel, this);
            }
            else
            {
                Name = itemPath;
                tlvModel.MasterList.Add(this);
                tlvModel.TLV.UpdateObjects(tlvModel.MasterList);
            }

            Path = itemPath;
            tlvModel.AbilityMasterList.Add(this);

            Id = abilityId;
            Name = "(" + Id.ToObjectId() + ") " + Name;

            if (null != image && !tlvModel.TLV.SmallImageList.Images.ContainsKey(itemPath))
            {
                ImageKey = itemPath;
                tlvModel.TLV.SmallImageList.Images.Add(ImageKey, image);
            }
        }

        internal static AbilityModel Find(TLVBase tlvModel, string path)
        {
            return tlvModel.AbilityMasterList.Find(x => x.Path == path);
        }

        internal static bool Exist(TLVBase tlvModel, string itemPath)
        {
            return tlvModel.AbilityMasterList.Exists(x => x.Path == itemPath);
        }

        internal static void ChangeIcon(TLVBase tlvModel, string itemPath, Image image)
        {
            Find(tlvModel, itemPath).ChangeIcon(tlvModel, image);
        }

        internal void ChangeIcon(TLVBase tlvModel, Image image)
        {
            if (null == image)
                ImageKey = null;

            else
            {
                tlvModel.TLV.SmallImageList.Images.RemoveByKey(ImageKey);
                tlvModel.TLV.SmallImageList.Images.Add(Path, image);
            }
        }
    }
}
