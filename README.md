# MDPI-Sensors---IOT-Education-ReactionTimeTool
A software developed to monitor reaction times of volunteers during watching of remote held lectures.

It has been developed for the experiment described in the paper Marceddu, A.C.; Pugliese, L.; Sini, J.; Espinosa, G.R.; Amel Solouki, M.; Chiavassa, P.; Giusto, E.; Montrucchio, B.; Violante, M.; De Pace, F. "A Novel Redundant Validation IoT System for Affective Learning Based on Facial Expressions and Biological Signals". Sensors 2022, 22, 2773. https://doi.org/10.3390/s22072773 
Please cite this repository as the "reaction time measurement tool" described in the aforementioned paper.

## Content of this repository
<i>MainWindow.xaml</i>: the main window of the application. It allows to choose where to save the log file, then shows a blinking hourglass to show its correct functioning to the user.
<i>MainWindow.xaml.cs</i>: the source code of main window of the application.
It contains the filed representing the array <i>_ElapsedSeconds</i> containing the times (expressed in second) of when to show the message window.

<i>MessageWindow.xaml</i>: the message window of the application. It allows the volunteers to register if they are or not paying attention to the lecture and measures their reaction times.
<i>MessageWindow.xaml.cs</i>: the source code of message window of the application.

<i>Belligerent.wav</i>: a notification sound. This file has not been produced by the author of this software, but it has been released by https://github.com/akx/Notifications under  Attribution 3.0 Unported (CC BY 3.0) (https://creativecommons.org/licenses/by/3.0/) licence.

Jacopo Sini, December 2021, Politecnico di Torino
