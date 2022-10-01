# FishConditionExpression
宝鱼的条件表达式解析工具，欢迎提issue

 - 支持：支持使用括号嵌套多层条件表达式
 - 局限: 
  1. 目前只支持and和or操作符(想换成别的符号也可以修改源码，在ConditionHelper.GetMode方法中)
  2. 间隔的符号是1个空格。

## 需自定义的部分：修改RawCondition的True方法实现即可
```Csharp
    public class RawCondition : ACondition
    {
        public string name;
        public override bool True()
        {
            return name == "a";//此处是简单判断，是a则返回True
        }
    }
```

## 使用方式
```Csharp
    var condition1 = ConditionHelper.ParseToConditon("(a and b) or b");
    Console.WriteLine(condition1.True());//返回False
    
    var condition2 = ConditionHelper.ParseToConditon("a or b or a");
    Console.WriteLine(condition2.True());//返回True
```
