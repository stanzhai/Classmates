namespace Classmates
{
    // 用于保存一个同学的所有信息
    public struct CInfo
    {
        public string id;
        public string name;
        public string sex;
        public string qq;
        public string addr;
        public string email;
        public string tel;
        public string phone;
        public string bday;
        public string words;
        public string pic;
    }
    // 用户查看所有同学记录或部分记录
    public struct SFind
    {
        public string name;
        public int index;
    }
}