# Xamarin_Forms_AreaSelectorSample
（可能是Github上第一个）基于Xamarin.Forms 5.0 的省市区三级联动示例

目前支持Android与iOS平台

省市区数据取自腾讯位置服务

使用前建议将key替换为自有密钥

# 已知问题
1. 当没有数据的时候，Picker会显示一个默认值0
2. ~~Picker在Android端的夜间模式（黑色背景）下，无法正常显示（可读性极差）~~（可通过设定TextColor属性来改善可读性）

# 致谢
PickerRenderer中的代码参考自amay077的Xamarin_Forms_PickerViewSample
