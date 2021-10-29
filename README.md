# Clock11

## What is Clock11?

Clock11 is a very small app which brings back the clock on all other taskbars except the primary taskbar.
In Windows 11 the taskbar was functionally reduced, so the clock is only on the primary taskbar. But if you have
multiple screens and e.g. you're playing a game in fullscreen, then you have no clock to look on. 

<a href='https://www.microsoft.com/en-us/p/clock11/9mvg23f3kc11#activetab=pivot:overviewtab'><img src='https://developer.microsoft.com/store/badges/images/English_get-it-from-MS.png' alt='English badge' width="150" /></a>

## Why Clock11?
![feedback.png](https://github.com/andyld97/Clock11/blob/main/Clock11/Assets/feedback.png)

## Problems

Since this is a WPF Application which spawns transparent and TopMost=true windows, it can happen that the clock(s)
disappear(s) in the background, especially if you are using the additional taskbars. I've tried to bring the windows back to the front, but it doesn't really work without interrupting the user. I think it is related to the fact that the taskbars are on top of all windows.

## Hotkeys
- `Ctrl + U` - Bring all clocks back to the front and restores the previous foreground window
- `Ctrl + H` - Hides all clock windows
