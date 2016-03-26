using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 扩展方法工具类
/// </summary>
public static class ExtentionMethods
{
    #region 数组
    /// <summary>
    /// 比较两数组值是否相同(考虑元素顺序)
    /// </summary>
    public static bool ValueEqual<T>(this T[] array1, T[] array2)
    {
        if (array1 == array2)
        {
            return true;
        }
        else if (array1 == null || array2 == null)
        {
            return false;
        }
        else if (array1.Length != array2.Length)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < array1.Length; i++)
            {
                if (!array1[i].Equals(array2[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }

    /// <summary>
    /// 截取子数组(建议使用List)
    /// </summary>
    /// <param name="startIndex">起始索引</param>
    public static T[] SubArray<T>(this T[] sourceArray, int startIndex)
    {
        if (sourceArray == null)
        {
            return null;
        }
        else
        {
            if (startIndex < 0)
            {
                startIndex = 0;
            }
            else if (startIndex > sourceArray.Length - 1)
            {
                startIndex = sourceArray.Length - 1;
            }
            T[] resultArray = new T[sourceArray.Length - startIndex];
            for (int i = 0; i < resultArray.Length; i++)
            {
                resultArray[i] = sourceArray[startIndex + i];
            }
            return resultArray;
        }

    }

    /// <summary>
    /// 截取子数组(建议使用List)
    /// </summary>
    /// <param name="startIndex">起始索引</param>
    /// <param name="length">截取长度</param>
    public static T[] SubArray<T>(this T[] sourceArray, int startIndex, int length)
    {
        if (sourceArray == null)
        {
            return null;
        }
        else
        {
            if (startIndex < 0)
            {
                startIndex = 0;
            }
            else if (startIndex > sourceArray.Length - 1)
            {
                startIndex = sourceArray.Length - 1;
            }
            if (length < 1)
            {
                length = 1;
            }
            else if (length > sourceArray.Length - startIndex)
            {
                length = sourceArray.Length - startIndex;
            }
            T[] resultArray = new T[length];
            for (int i = 0; i < resultArray.Length; i++)
            {
                resultArray[i] = sourceArray[startIndex + i];
            }
            return resultArray;
        }
    }

    /// <summary>
    /// 获取单字符串形式的数组元素集合
    /// </summary>
    /// <param name="spliter">元素分隔符</param>
    public static string ToSingleString<T>(this T[] sourceArray, char spliter)
    {
        if (sourceArray == null)
        {
            return null;
        }
        else if (sourceArray.Length == 0)
        {
            return string.Empty;
        }
        else
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < sourceArray.Length; i++)
            {
                builder.Append(sourceArray[i]);
                builder.Append(spliter);
            }
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }
    }
    #endregion

    #region 字典
    /// <summary>
    /// 添加键值对，若键已存在则覆盖原值
    /// </summary>
    public static void AddAfterCheck<T, K>(this Dictionary<T, K> sourceDic, T key, K value)
    {
        if (sourceDic.ContainsKey(key))
        {
            sourceDic[key] = value;
        }
        else
        {
            sourceDic.Add(key, value);
        }
    }

    /// <summary>
    /// 根据键获取值，若键不存在则返回K的默认值
    /// </summary>
    public static K GetAfterCheck<T, K>(this Dictionary<T, K> sourceDic, T key)
    {
        if (sourceDic.ContainsKey(key))
        {
            return sourceDic[key];
        }
        else
        {
            return default(K);
        }
    }
    #endregion

    #region List
    /// <summary>
    /// 获取单字符串形式的列表元素集合
    /// </summary>
    /// <param name="spliter">元素分隔符</param>
    public static string ToSingleString<T>(this List<T> sourceList, char spliter)
    {
        if (sourceList == null)
        {
            return null;
        }
        else if (sourceList.Count == 0)
        {
            return string.Empty;
        }
        else
        {
            StringBuilder builder = new StringBuilder();
            sourceList.ForEach(item =>
            {
                builder.Append(item);
                builder.Append(spliter);
            });
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }
    }
    #endregion

    #region GameObject
    /// <summary>
    /// 移除所有子游戏物体
    /// </summary>
    public static void RemoveAllChildren(this GameObject gameObject)
    {
        Transform transform = gameObject.transform;
        for (int i = 0; i < transform.childCount; i++)
        {
            //防止一个奇怪的dotween空指针bug使用，GuideLineView -> uiText
            if (transform.GetChild(i).name.Equals("LookOutText")) continue;

            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 设置自身和自身所有子物体的Layer
    /// </summary>
    public static void SetChildrenLayers(this GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
        Transform transform = gameObject.transform;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetChildrenLayers(layer);
        }
    }
    #endregion

    #region Transform
    ///// <summary>
    ///// 调整物体及子物体的所有UIWight层级,不包含UIPanel
    ///// </summary>
    //public static void AdjustWidghtDepth(this Transform trans, int adjustment)
    //{
    //    if (trans != null)
    //    {
    //        UIPanel panel = trans.GetComponent<UIPanel>();
    //        if (panel == null)
    //        {
    //            UIWidget widght = trans.GetComponent<UIWidget>();
    //            if (widght != null)
    //            {
    //                widght.depth += adjustment;
    //            }
    //        }
    //        if (trans.childCount != 0)
    //        {
    //            for (int i = 0; i < trans.childCount; i++)
    //            {
    //                trans.GetChild(i).AdjustWidghtDepth(adjustment);
    //            }
    //        }
    //    }
    //}
    #endregion

    #region ISFSObject
    //public static string ToSingleString(this ISFSObject data)
    //{
    //    if (data == null)
    //    {
    //        return null;
    //    }
    //    else
    //    {
    //        string[] keys = data.GetKeys();
    //        StringBuilder builder = new StringBuilder();
    //        for (int i = 0; i < keys.Length; i++)
    //        {
    //            builder.Append(keys[i]);
    //            builder.Append(":");
    //            SFSDataWrapper model = data.GetData(keys[i]);
    //            switch ((SFSDataType)model.Type)
    //            {
    //                case SFSDataType.BOOL:
    //                    builder.Append((bool)model.Data);
    //                    break;
    //                case SFSDataType.BOOL_ARRAY:
    //                    builder.Append(((bool[])model.Data).ToSingleString(','));
    //                    break;
    //                case SFSDataType.BYTE:
    //                    builder.Append((byte)model.Data);
    //                    break;
    //                case SFSDataType.BYTE_ARRAY:
    //                    builder.Append(((byte[])model.Data).ToSingleString(','));
    //                    break;
    //                case SFSDataType.CLASS:
    //                    builder.Append("class,skip!");
    //                    break;
    //                case SFSDataType.DOUBLE:
    //                    builder.Append((double)model.Data);
    //                    break;
    //                case SFSDataType.DOUBLE_ARRAY:
    //                    builder.Append(((double[])model.Data).ToSingleString(','));
    //                    break;
    //                case SFSDataType.FLOAT:
    //                    builder.Append((float)model.Data);
    //                    break;
    //                case SFSDataType.FLOAT_ARRAY:
    //                    builder.Append(((float[])model.Data).ToSingleString(','));
    //                    break;
    //                case SFSDataType.INT:
    //                    builder.Append((int)model.Data);
    //                    break;
    //                case SFSDataType.INT_ARRAY:
    //                    builder.Append(((int[])model.Data).ToSingleString(','));
    //                    break;
    //                case SFSDataType.LONG:
    //                    builder.Append((long)model.Data);
    //                    break;
    //                case SFSDataType.LONG_ARRAY:
    //                    builder.Append(((long[])model.Data).ToSingleString(','));
    //                    break;
    //                case SFSDataType.NULL:
    //                    builder.Append("NULL");
    //                    break;
    //                case SFSDataType.SFS_ARRAY:
    //                    builder.Append("SFSArray,skip!");
    //                    break;
    //                case SFSDataType.SFS_OBJECT:
    //                    builder.AppendLine();
    //                    builder.AppendLine("--------");
    //                    builder.Append(((SFSObject)model.Data).ToSingleString());
    //                    builder.AppendLine("--------");
    //                    break;
    //                case SFSDataType.SHORT:
    //                    builder.Append((short)model.Data);
    //                    break;
    //                case SFSDataType.SHORT_ARRAY:
    //                    builder.Append(((short[])model.Data).ToSingleString(','));
    //                    break;
    //                case SFSDataType.UTF_STRING:
    //                    builder.Append((string)model.Data);
    //                    break;
    //                case SFSDataType.UTF_STRING_ARRAY:
    //                    builder.Append(((string[])model.Data).ToSingleString(','));
    //                    break;
    //                default:
    //                    builder.Append("unknown,skip!");
    //                    break;
    //            }
    //            builder.AppendLine();
    //        }
    //        return builder.ToString();
    //    }
    //}
    #endregion

    #region Widget
    ///// <summary>
    ///// UIWidget.color补间动画
    ///// </summary>
    ///// <param name="endValue">终值</param>
    ///// <param name="duration">持续时间</param>
    //public static Tweener DOColor(this UIWidget target, Color endValue, float duration)
    //{
    //    return DOTween.To(() => target.color, color => target.color = color, endValue, duration).SetTarget<Tweener>(target.transform);
    //}

    ///// <summary>
    ///// UIWidget.alpha补间动画
    ///// </summary>
    ///// <param name="endValue">终值</param>
    ///// <param name="duration">持续时间</param>
    ///// <returns></returns>
    //public static Tweener DOAlpha(this UIWidget target, float endValue, float duration)
    //{
    //    return DOTween.To(() => target.alpha, alpha => target.alpha = alpha, endValue, duration).SetTarget<Tweener>(target.transform);
    //}
    #endregion

    #region Button
    public static void DisableSeconds(this Button button, float duration = 1f)
    {
        button.interactable = false;
        button.StartCoroutine( DelayToInvoke.DelayToInvokeDo(() => {
            button.interactable = true;
        }, duration));
    }
    #endregion

    #region String
    public static string UrlEncode(this string str, Encoding encoding)
    {
        StringBuilder sb = new StringBuilder();
        byte[] byStr = encoding.GetBytes(str); //默认System.Text.Encoding.Default.GetBytes(str)
        for (int i = 0; i < byStr.Length; i++)
        {
            sb.Append(@"%" + Convert.ToString(byStr[i], 16));
        }
        return (sb.ToString());
    }
    #endregion
}

public class DelayToInvoke : MonoBehaviour
{
    public static IEnumerator DelayToInvokeDo(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }
}
