# 📌 Changelog 更新日志

本项目的所有更新记录都会列在这里。版本从新到旧排列。

---

## [v0.2.0] - 2025-06-15

### ✨ 新增
- 添加 `CostomFonText.cs`：用于根据文本段落控制组件激活，支持扩展功能
- 添加 `CONTRIBUTING.md`：项目贡献指南，欢迎开源协作
- 添加 `CHANGELOG.md`：更新日志，记录每次更动

### 🛠 修复
- 修复 `ThirdPersonCamera.cs` 中摄像机旋转抖动的问题
- 修复跳跃逻辑在某些场景下未生效的 bug（`PlayerMovement.cs`）

### 📦 改动
- 所有脚本文件重新整理到 `Scripts/` 文件夹下
  - `Scripts/Player/PlayerMovement.cs`
  - `Scripts/Camera/ThirdPersonCamera.cs`
  - `Scripts/UI/PauseMenu.cs`
  - `Scripts/System/MouseLook.cs`
  - `Scripts/UI/CostomFonText.cs`
- 优化 `PauseMenu.cs` 的菜单布局逻辑，准备加入 Resume 功能

---

## [v0.1.0] - 2025-03-10

### ✨ 初始版本发布
- `MouseLook.cs`：使用鼠标控制游戏视角
- `ThirdPersonCamera.cs`：第三人称相机控制，带平滑处理
- `PlayerMovement.cs`：基础的角色移动、跳跃（需要 Rigidbody 组件）
- `PauseMenu.cs`：按 ESC 呼出菜单，可暂停和退出游戏

