# 这里是Unity脚本库

<div align="center">

**⚠️重要通知⚠️**


由于unity引擎更新，本脚本库使用的旧式函数可能无法适配最新的unity引擎


现在正在逐步修改

</div>

## 目录
- ### [角色行动](角色行动)
- ### [摄像机控制](摄像机控制)
- ### [UI](UI)
- ### [逻辑相关](逻辑相关)

## 角色行动
- **[PlayerMovement](./Unity/PlayerMovement.cs)**：基础角色控制，实现前后左右移动和跳跃（需要 Rigidbody）。

## 摄像机控制
- **[MouseLook](./Unity/MouseLook.cs)**：使用鼠标控制游戏视角的移动。
- **[ThirdPersonCamera](./Unity/ThirdPersonCamera.cs)**：第三人称视角的摄像机控制，带有平滑跟随效果。

## UI
- **[PauseMenu](./Unity/PauseMenu.cs)**：按下 ESC 弹出菜单，包含继续和退出游戏。


## 逻辑相关
- **[CustomFonText](./Unity/CustomFonText.cs)**：根据文本进度控制组件激活/隐藏的万能脚本，功能强大，可拓展性强。
