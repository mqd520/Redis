using System;
using System.Collections.Generic;
using System.Text;
using ServiceStack.Redis;

namespace ServiceStackHelper
{
    public class ServiceStackHelper : IDisposable
    {
        /*
         * 以下方法为基本的设置数据和取数据
         */
        private static RedisClient redisCli = null;
        /// <summary>
        /// 建立redis长连接
        /// </summary>
        /// 将此处的IP换为自己的redis实例IP，如果设有密码，第三个参数为密码，string 类型
        public static void CreateClient(string hostIP, int port, string keyword)
        {
            if (redisCli == null)
            {
                redisCli = new RedisClient(hostIP, port, keyword);
            }
        }
        public static void CreateClient(string hostIP, int port)
        {
            if (redisCli == null)
            {
                redisCli = new RedisClient(hostIP, port);
            }
        }
        /// <summary>
        /// 获取key,返回string格式
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string getValueString(string key)
        {
            string value = redisCli.GetValue(key);
            return value;
        }
        /// <summary>
        /// 获取key,返回byte[]格式
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] getValueByte(string key)
        {
            byte[] value = redisCli.Get(key);
            return value;
        }
        /// <summary>
        /// 获得某个hash型key下的所有字段
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public static List<string> GetHashFields(string hashId)
        {
            List<string> hashFields = redisCli.GetHashKeys(hashId);
            return hashFields;
        }
        /// <summary>
        /// 获得某个hash型key下的所有值
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public static List<string> GetHashValues(string hashId)
        {
            List<string> hashValues = redisCli.GetHashKeys(hashId);
            return hashValues;
        }
        /// <summary>
        /// 获得hash型key某个字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        public static string GetHashField(string key, string field)
        {
            string value = redisCli.GetValueFromHash(key, field);
            return value;
        }
        /// <summary>
        /// 设置hash型key某个字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public static void SetHashField(string key, string field, string value)
        {
            redisCli.SetEntryInHash(key, field, value);
        }
        /// <summary>
        ///使某个字段增加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static void SetHashIncr(string key, string field, long incre)
        {
            redisCli.IncrementValueInHash(key, field, incre);
        }
        /// <summary>
        /// 向list类型数据添加成员，向列表底部(右侧)添加
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="list"></param>
        public static void AddItemToListRight(string list, string item)
        {
            redisCli.AddItemToList(list, item);
        }
        /// <summary>
        /// 向list类型数据添加成员，向列表顶部(左侧)添加
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        public static void AddItemToListLeft(string list, string item)
        {
            redisCli.LPush(list, Encoding.Default.GetBytes(item));
        }
        /// <summary>
        /// 从list类型数据读取所有成员
        /// </summary>
        public static List<string> GetAllItems(string list)
        {
            List<string> listMembers = redisCli.GetAllItemsFromList(list);
            return listMembers;
        }
        /// <summary>
        /// 从list类型数据指定索引处获取数据，支持正索引和负索引
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetItemFromList(string list, int index)
        {
            string item = redisCli.GetItemFromList(list, index);
            return item;
        }
        /// <summary>
        /// 向列表底部（右侧）批量添加数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="values"></param>
        public static void GetRangeToList(string list, List<string> values)
        {
            redisCli.AddRangeToList(list, values);
        }
        /// <summary>
        /// 向集合中添加数据
        /// </summary>
        /// <param name="item"></param>
        /// <param name="set"></param>
        public static void GetItemToSet(string item, string set)
        {
            redisCli.AddItemToSet(item, set);
        }
        /// <summary>
        /// 获得集合中所有数据
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public static HashSet<string> GetAllItemsFromSet(string set)
        {
            HashSet<string> items = redisCli.GetAllItemsFromSet(set);
            return items;
        }
        /// <summary>
        /// 获取fromSet集合和其他集合不同的数据
        /// </summary>
        /// <param name="fromSet"></param>
        /// <param name="toSet"></param>
        /// <returns></returns>
        public static HashSet<string> GetSetDiff(string fromSet, params string[] toSet)
        {
            HashSet<string> diff = redisCli.GetDifferencesFromSet(fromSet, toSet);
            return diff;
        }
        /// <summary>
        /// 获得所有集合的并集
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public static HashSet<string> GetSetUnion(params string[] set)
        {
            HashSet<string> union = redisCli.GetUnionFromSets(set);
            return union;
        }
        /// <summary>
        /// 获得所有集合的交集
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public static HashSet<string> GetSetInter(params string[] set)
        {
            HashSet<string> inter = redisCli.GetIntersectFromSets(set);
            return inter;
        }
        /// <summary>
        /// 向有序集合中添加元素
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        public static void AddItemToSortedSet(string set, string value, long score)
        {
            redisCli.AddItemToSortedSet(set, value, score);
        }
        /// <summary>
        /// 获得某个值在有序集合中的排名，按分数的降序排列
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetItemIndexInSortedSetDesc(string set, string value)
        {
            int index = redisCli.GetItemIndexInSortedSetDesc(set, value);
            return index;
        }
        /// <summary>
        /// 获得某个值在有序集合中的排名，按分数的升序排列
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetItemIndexInSortedSet(string set, string value)
        {
            int index = redisCli.GetItemIndexInSortedSet(set, value);
            return index;
        }
        /// <summary>
        /// 获得有序集合中某个值得分数
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double GetItemScoreInSortedSet(string set, string value)
        {
            double score = redisCli.GetItemScoreInSortedSet(set, value);
            return score;
        }
        /// <summary>
        /// 获得有序集合中，某个排名范围的所有值
        /// </summary>
        /// <param name="set"></param>
        /// <param name="beginRank"></param>
        /// <param name="endRank"></param>
        /// <returns></returns>
        public static List<string> GetRangeFromSortedSet(string set, int beginRank, int endRank)
        {
            List<string> valueList = redisCli.GetRangeFromSortedSet(set, beginRank, endRank);
            return valueList;
        }
        /// <summary>
        /// 获得有序集合中，某个分数范围内的所有值，升序
        /// </summary>
        /// <param name="set"></param>
        /// <param name="beginScore"></param>
        /// <param name="endScore"></param>
        /// <returns></returns>
        public static List<string> GetRangeFromSortedSet(string set, double beginScore, double endScore)
        {
            List<string> valueList = redisCli.GetRangeFromSortedSetByHighestScore(set, beginScore, endScore);
            return valueList;
        }
        /// <summary>
        /// 获得有序集合中，某个分数范围内的所有值，降序
        /// </summary>
        /// <param name="set"></param>
        /// <param name="beginScore"></param>
        /// <param name="endScore"></param>
        /// <returns></returns>
        public static List<string> GetRangeFromSortedSetDesc(string set, double beginScore, double endScore)
        {
            List<string> vlaueList = redisCli.GetRangeFromSortedSetByLowestScore(set, beginScore, endScore);
            return vlaueList;
        }
        public void Dispose()
        {
            redisCli.Dispose();
        }
    }
}
