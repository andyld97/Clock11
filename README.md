# Clock11

## What is Clock11?

Clock11 is a very small app which brings back the clock on all other taskbars except the primary taskbar.
In Windows 11 the taskbar was functionally reduced, so the clock is only on the primary taskbar. But if you have
multiple screens and e.g. you're playing a game in fullscreen, then you have no clock to look on. 

## Why Clock11?
![feedback.png](https://github.com/andyld97/Clock11/blob/main/Clock11/Assets/feedback.png)

## How To Install
Just look at the "Release" page, there you can download the setup-file which you can install.
The setup automatically puts Clock11 into the startup folder (shell:startup)!

## Problems

Since this is a WPF Application which spawns transparent and TopMost=true windows, it can happen that the clock(s)
disappears in the background, especially if you are using the additional taskbars. I've tried to bring the windows back to the front, but it doesn't really work without interrupting the user. I think it is related to the fact that the taskbars are on top of all windows.

## Hotkeys
`Ctrl + U` - Bring all clocks back to the front 
