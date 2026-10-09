# Sedentary-reminder 久坐提醒小工具

## Repo rosters
[![Stargazers repo roster for @wjbgis/Sedentary-reminder](https://reporoster.com/stars/wjbgis/Sedentary-reminder)](https://github.com/wjbgis/Sedentary-reminder/stargazers)
[![Forkers repo roster for @wjbgis/Sedentary-reminder](https://reporoster.com/forks/wjbgis/Sedentary-reminder)](https://github.com/wjbgis/Sedentary-reminder/network/members)

## 下载  

[Download](https://github.com/wjbgis/Sedentary-reminder/releases)

## 介绍

![](https://github.com/wjbgis/Sedentary-reminder/blob/master/ScreenShot/0.png)

​	偶然看到人民日报公众号这篇文章，如果数字相对准确，那确实有点震惊。感觉自己明知久坐有害，但就是不自觉，电脑前一坐就是一上午、一下午。于是就想是否有一款软件能定时提醒自己不要久坐，网上搜寻了半天，感觉找到的软件都不能“完全阻止”我久坐的行为，那干脆自己写一个算了。

---

* 主界面很简单，也很丑，用的WinForm

  ![](https://github.com/wjbgis/Sedentary-reminder/blob/master/ScreenShot/1.png)

* 点击`开始`之后就开始倒计时

  ![](https://github.com/wjbgis/Sedentary-reminder/blob/master/ScreenShot/2.1.png)

* 工作倒计时剩余15秒时，提示用户即将锁定输入

  ![](https://github.com/wjbgis/Sedentary-reminder/blob/master/ScreenShot/4.png)

* 倒计时结束，显示遮罩层，**屏蔽键盘**（`Alt`+`F4`、`Win`、`Alt`+`Tab`、`Ctrl`+`Shift`+`Esc` 等全部无效；**鼠标保持可用**），遮罩覆盖所有显示器；为防止意外，系统保留组合键 `ctrl`+`alt`+`del` 无法拦截，仍可通过它关机或锁屏

  ![](https://github.com/wjbgis/Sedentary-reminder/blob/master/ScreenShot/3.png)

* 遮罩上提供"立即解锁，继续工作"按钮，特殊情况可跳过本次剩余休息

* 休息时会随机显示一条健康提示语，可在设置界面自定义（每行一条，留空使用内置默认）

* 休息结束，继续开始工作倒计时

* 其它可配置项：

  * **开机自动启动**
  * **限定工作时间**：可设置两个时段（如 09:00-12:00、13:30-18:00，支持跨夜），非工作时段自动暂停计时，进入时段后自动恢复
  * 系统休眠/锁屏期间不计时，避免"睡一觉回来发现该休息了"

* **支持系统：Win7/10**

## 致谢

感谢 [netnr](https://github.com/netnr) 提的宝贵建议，让我有了继续更新的动力

