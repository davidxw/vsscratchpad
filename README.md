# Scratchpad
A simple pinnable scratchpad window for Visual Studio.  

Notes in the scratchpad are saved to a text file in the default Documents directory, and are synchronised between instances of Visual Studio. Cut, copy and paste into the scratchpad with the usual mouse right click or keyboard shortcuts.  Crtl-A to select everything in the scratchpad.

Open the scratchpad from the "View" -> "Other Windows" menu, or use the keyboard shortcut Ctl-\\, Ctl-\\

Comments or issues in the github repo.

Fixes in version 1.4:

* Attempt to maintain cursor position when multiple instances of VS are updating the scratchpad
* Fixed keyboard short cut (Ctl-\\, Ctl-\\)
* Fixed occasional file write error on initialization
* Updated package base class to implement AsyncPackage rather than Package
