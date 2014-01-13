rmdir /s /q chunks
mkdir chunks
cd chunks
ffmpeg -i ..\face-converted.avi -ss 14.623 -t 23.466 -qscale 0 chunk002.avi
ffmpeg -i ..\face-converted.avi -ss 47.567 -t 8.111 -qscale 0 chunk004.avi
ffmpeg -i ..\face-converted.avi -ss 98.988 -t 5.407 -qscale 0 chunk006.avi
ffmpeg -i ..\face-converted.avi -ss 117.882 -t 13.186 -qscale 0 chunk008.avi
ffmpeg -i ..\face-converted.avi -ss 137.614 -t 1.895 -qscale 0 chunk010.avi
ffmpeg -i ..\face-converted.avi -ss 143.381 -t 8.143 -qscale 0 chunk012.avi
ffmpeg -i ..\face-converted.avi -ss 164.993 -t 6.494 -qscale 0 chunk014.avi
ffmpeg -i ..\face-converted.avi -ss 175.301 -t 4.272 -qscale 0 chunk016.avi
ffmpeg -i ..\face-converted.avi -ss 190.456 -t 26.091 -qscale 0 chunk018.avi
ffmpeg -i ..\face-converted.avi -ss 227.369 -t 17.788 -qscale 0 chunk020.avi
ffmpeg -i ..\face-converted.avi -ss 284.061 -t 13.327 -qscale 0 chunk022.avi
ffmpeg -i ..\face-converted.avi -ss 303.685 -t 31.977 -qscale 0 chunk024.avi
ffmpeg -i ..\face-converted.avi -ss 350.094 -t 2.627 -vn -qscale 0 audio026.avi
ffmpeg -i ..\desktop-converted.avi -ss 335.471 -t 2.627 -qscale 0 video026.avi
ffmpeg -i audio026.avi -i video026.avi -acodec copy -vcodec copy chunk026.avi
ffmpeg -i ..\face-converted.avi -ss 372.476 -t 2.781 -qscale 0 chunk028.avi
ffmpeg -i ..\face-converted.avi -ss 412.588 -t 1.985 -qscale 0 chunk030.avi
ffmpeg -i ..\face-converted.avi -ss 485.399 -t 2.787 -vn -qscale 0 audio032.avi
ffmpeg -i ..\desktop-converted.avi -ss 470.776 -t 2.787 -qscale 0 video032.avi
ffmpeg -i audio032.avi -i video032.avi -acodec copy -vcodec copy chunk032.avi
ffmpeg -i ..\face-converted.avi -ss 497.365 -t 3.508 -vn -qscale 0 audio034.avi
ffmpeg -i ..\desktop-converted.avi -ss 482.742 -t 3.508 -qscale 0 video034.avi
ffmpeg -i audio034.avi -i video034.avi -acodec copy -vcodec copy chunk034.avi
ffmpeg -i ..\face-converted.avi -ss 505.162 -t 4.751 -qscale 0 chunk036.avi
ffmpeg -i ..\face-converted.avi -ss 518.319 -t 5.545 -qscale 0 chunk038.avi
ffmpeg -i ..\face-converted.avi -ss 533.973 -t 2.101 -qscale 0 chunk040.avi
ffmpeg -i ..\face-converted.avi -ss 536.074 -t 8.259 -qscale 0 chunk041.avi
ffmpeg -i ..\face-converted.avi -ss 544.333 -t 3.384 -vn -qscale 0 audio042.avi
ffmpeg -i ..\desktop-converted.avi -ss 529.710 -t 3.384 -qscale 0 video042.avi
ffmpeg -i audio042.avi -i video042.avi -acodec copy -vcodec copy chunk042.avi
ffmpeg -i ..\face-converted.avi -ss 558.776 -t 7.257 -qscale 0 chunk044.avi
ffmpeg -i ..\face-converted.avi -ss 566.033 -t 34.069 -vn -qscale 0 audio045.avi
ffmpeg -i ..\desktop-converted.avi -ss 551.410 -t 34.069 -qscale 0 video045.avi
ffmpeg -i audio045.avi -i video045.avi -acodec copy -vcodec copy chunk045.avi
ffmpeg -i ..\face-converted.avi -ss 600.102 -t 11.601 -qscale 0 chunk046.avi
ffmpeg -i ..\face-converted.avi -ss 622.327 -t 10.353 -vn -qscale 0 audio048.avi
ffmpeg -i ..\desktop-converted.avi -ss 607.704 -t 10.353 -qscale 0 video048.avi
ffmpeg -i audio048.avi -i video048.avi -acodec copy -vcodec copy chunk048.avi
ffmpeg -i ..\face-converted.avi -ss 652.885 -t 12.045 -qscale 0 chunk050.avi
ffmpeg -i ..\face-converted.avi -ss 680.692 -t 8.037 -qscale 0 chunk052.avi
ffmpeg -i ..\face-converted.avi -ss 688.729 -t 10.126 -qscale 0 chunk053.avi
ffmpeg -i ..\face-converted.avi -ss 712.258 -t 4.547 -vn -qscale 0 audio055.avi
ffmpeg -i ..\desktop-converted.avi -ss 697.635 -t 4.547 -qscale 0 video055.avi
ffmpeg -i audio055.avi -i video055.avi -acodec copy -vcodec copy chunk055.avi
ffmpeg -i ..\face-converted.avi -ss 721.155 -t 12.282 -vn -qscale 0 audio057.avi
ffmpeg -i ..\desktop-converted.avi -ss 706.532 -t 12.282 -qscale 0 video057.avi
ffmpeg -i audio057.avi -i video057.avi -acodec copy -vcodec copy chunk057.avi
ffmpeg -i ..\face-converted.avi -ss 746.757 -t 5.514 -vn -qscale 0 audio059.avi
ffmpeg -i ..\desktop-converted.avi -ss 732.134 -t 5.514 -qscale 0 video059.avi
ffmpeg -i audio059.avi -i video059.avi -acodec copy -vcodec copy chunk059.avi
ffmpeg -i ..\face-converted.avi -ss 760.995 -t 21.956 -vn -qscale 0 audio061.avi
ffmpeg -i ..\desktop-converted.avi -ss 746.372 -t 21.956 -qscale 0 video061.avi
ffmpeg -i audio061.avi -i video061.avi -acodec copy -vcodec copy chunk061.avi
ffmpeg -i ..\face-converted.avi -ss 790.487 -t 5.574 -vn -qscale 0 audio063.avi
ffmpeg -i ..\desktop-converted.avi -ss 775.864 -t 5.574 -qscale 0 video063.avi
ffmpeg -i audio063.avi -i video063.avi -acodec copy -vcodec copy chunk063.avi
ffmpeg -i ..\face-converted.avi -ss 856.582 -t 10.372 -vn -qscale 0 audio065.avi
ffmpeg -i ..\desktop-converted.avi -ss 841.959 -t 10.372 -qscale 0 video065.avi
ffmpeg -i audio065.avi -i video065.avi -acodec copy -vcodec copy chunk065.avi
ffmpeg -i ..\face-converted.avi -ss 896.334 -t 7.185 -vn -qscale 0 audio067.avi
ffmpeg -i ..\desktop-converted.avi -ss 881.711 -t 7.185 -qscale 0 video067.avi
ffmpeg -i audio067.avi -i video067.avi -acodec copy -vcodec copy chunk067.avi
ffmpeg -i ..\face-converted.avi -ss 908.800 -t 5.104 -vn -qscale 0 audio069.avi
ffmpeg -i ..\desktop-converted.avi -ss 894.177 -t 5.104 -qscale 0 video069.avi
ffmpeg -i audio069.avi -i video069.avi -acodec copy -vcodec copy chunk069.avi
ffmpeg -i ..\face-converted.avi -ss 966.301 -t 14.863 -qscale 0 chunk071.avi
ffmpeg -i ..\face-converted.avi -ss 987.442 -t 4.289 -qscale 0 chunk073.avi
ffmpeg -i ..\face-converted.avi -ss 997.879 -t 40.006 -qscale 0 chunk075.avi
ffmpeg -i ..\face-converted.avi -ss 1037.885 -t 0.000 -qscale 0 chunk076.avi
ffmpeg -i ..\face-converted.avi -ss 1054.358 -t 22.384 -qscale 0 chunk078.avi
ffmpeg -i ..\face-converted.avi -ss 1096.132 -t 17.495 -qscale 0 chunk080.avi
ffmpeg -i ..\face-converted.avi -ss 1113.627 -t 18.958 -qscale 0 chunk081.avi
ffmpeg -i ..\face-converted.avi -ss 1132.585 -t 32.576 -vn -qscale 0 audio082.avi
ffmpeg -i ..\desktop-converted.avi -ss 1117.962 -t 32.576 -qscale 0 video082.avi
ffmpeg -i audio082.avi -i video082.avi -acodec copy -vcodec copy chunk082.avi
ffmpeg -i ..\face-converted.avi -ss 1165.161 -t 7.980 -qscale 0 chunk083.avi
ffmpeg -i ..\face-converted.avi -ss 1180.858 -t 15.867 -vn -qscale 0 audio085.avi
ffmpeg -i ..\desktop-converted.avi -ss 1166.235 -t 15.867 -qscale 0 video085.avi
ffmpeg -i audio085.avi -i video085.avi -acodec copy -vcodec copy chunk085.avi
ffmpeg -i ..\face-converted.avi -ss 1204.108 -t 11.452 -qscale 0 chunk087.avi
ffmpeg -i ..\face-converted.avi -ss 1222.231 -t 3.571 -qscale 0 chunk089.avi
ffmpeg -i ..\face-converted.avi -ss 1263.812 -t 15.306 -qscale 0 chunk091.avi
ffmpeg -i ..\face-converted.avi -ss 1297.340 -t 4.248 -qscale 0 chunk093.avi
ffmpeg -i ..\face-converted.avi -ss 1357.388 -t 48.127 -qscale 0 chunk095.avi
ffmpeg -i ..\face-converted.avi -ss 1405.515 -t 8.032 -vn -qscale 0 audio096.avi
ffmpeg -i ..\desktop-converted.avi -ss 1390.892 -t 8.032 -qscale 0 video096.avi
ffmpeg -i audio096.avi -i video096.avi -acodec copy -vcodec copy chunk096.avi
ffmpeg -i ..\face-converted.avi -ss 1441.592 -t 3.333 -qscale 0 chunk098.avi
ffmpeg -i ..\face-converted.avi -ss 1449.619 -t 16.043 -qscale 0 chunk100.avi
ffmpeg -i ..\face-converted.avi -ss 1483.413 -t 18.176 -vn -qscale 0 audio102.avi
ffmpeg -i ..\desktop-converted.avi -ss 1468.790 -t 18.176 -qscale 0 video102.avi
ffmpeg -i audio102.avi -i video102.avi -acodec copy -vcodec copy chunk102.avi
ffmpeg -i ..\face-converted.avi -ss 1518.073 -t 3.793 -vn -qscale 0 audio104.avi
ffmpeg -i ..\desktop-converted.avi -ss 1503.450 -t 3.793 -qscale 0 video104.avi
ffmpeg -i audio104.avi -i video104.avi -acodec copy -vcodec copy chunk104.avi
ffmpeg -i ..\face-converted.avi -ss 1570.690 -t 15.210 -vn -qscale 0 audio106.avi
ffmpeg -i ..\desktop-converted.avi -ss 1556.067 -t 15.210 -qscale 0 video106.avi
ffmpeg -i audio106.avi -i video106.avi -acodec copy -vcodec copy chunk106.avi
ffmpeg -i ..\face-converted.avi -ss 1585.900 -t 71.115 -qscale 0 chunk107.avi
ffmpeg -i ..\face-converted.avi -ss 1661.643 -t 22.444 -vn -qscale 0 audio109.avi
ffmpeg -i ..\desktop-converted.avi -ss 1647.020 -t 22.444 -qscale 0 video109.avi
ffmpeg -i audio109.avi -i video109.avi -acodec copy -vcodec copy chunk109.avi
ffmpeg -i ..\face-converted.avi -ss 1733.411 -t 2.977 -vn -qscale 0 audio111.avi
ffmpeg -i ..\desktop-converted.avi -ss 1718.788 -t 2.977 -qscale 0 video111.avi
ffmpeg -i audio111.avi -i video111.avi -acodec copy -vcodec copy chunk111.avi
ffmpeg -i ..\face-converted.avi -ss 1794.813 -t 3.121 -vn -qscale 0 audio113.avi
ffmpeg -i ..\desktop-converted.avi -ss 1780.190 -t 3.121 -qscale 0 video113.avi
ffmpeg -i audio113.avi -i video113.avi -acodec copy -vcodec copy chunk113.avi
ffmpeg -i ..\face-converted.avi -ss 1810.117 -t 1.328 -vn -qscale 0 audio115.avi
ffmpeg -i ..\desktop-converted.avi -ss 1795.494 -t 1.328 -qscale 0 video115.avi
ffmpeg -i audio115.avi -i video115.avi -acodec copy -vcodec copy chunk115.avi
ffmpeg -i ..\face-converted.avi -ss 1824.760 -t 9.072 -vn -qscale 0 audio117.avi
ffmpeg -i ..\desktop-converted.avi -ss 1810.137 -t 9.072 -qscale 0 video117.avi
ffmpeg -i audio117.avi -i video117.avi -acodec copy -vcodec copy chunk117.avi
ffmpeg -i ..\face-converted.avi -ss 1853.455 -t 8.467 -vn -qscale 0 audio119.avi
ffmpeg -i ..\desktop-converted.avi -ss 1838.832 -t 8.467 -qscale 0 video119.avi
ffmpeg -i audio119.avi -i video119.avi -acodec copy -vcodec copy chunk119.avi
ffmpeg -i ..\face-converted.avi -ss 1869.013 -t 3.511 -vn -qscale 0 audio121.avi
ffmpeg -i ..\desktop-converted.avi -ss 1854.390 -t 3.511 -qscale 0 video121.avi
ffmpeg -i audio121.avi -i video121.avi -acodec copy -vcodec copy chunk121.avi
ffmpeg -i ..\face-converted.avi -ss 1894.772 -t 3.670 -vn -qscale 0 audio123.avi
ffmpeg -i ..\desktop-converted.avi -ss 1880.149 -t 3.670 -qscale 0 video123.avi
ffmpeg -i audio123.avi -i video123.avi -acodec copy -vcodec copy chunk123.avi
ffmpeg -i ..\face-converted.avi -ss 1957.519 -t 34.403 -qscale 0 chunk125.avi
ffmpeg -i ..\face-converted.avi -ss 2019.134 -t 54.228 -qscale 0 chunk127.avi
ffmpeg -i ..\face-converted.avi -ss 2073.362 -t 0.000 -qscale 0 chunk128.avi
ffmpeg -i ..\face-converted.avi -ss 2087.424 -t 48.281 -qscale 0 chunk130.avi
ffmpeg -i ..\face-converted.avi -ss 2208.744 -t 13.179 -qscale 0 chunk132.avi
ffmpeg -i ..\face-converted.avi -ss 2221.923 -t 50.898 -qscale 0 chunk133.avi
ffmpeg -i ..\face-converted.avi -ss 2275.517 -t 7.656 -vn -qscale 0 audio135.avi
ffmpeg -i ..\desktop-converted.avi -ss 2260.894 -t 7.656 -qscale 0 video135.avi
ffmpeg -i audio135.avi -i video135.avi -acodec copy -vcodec copy chunk135.avi
ffmpeg -i ..\face-converted.avi -ss 2283.173 -t 81.925 -vn -qscale 0 audio136.avi
ffmpeg -i ..\desktop-converted.avi -ss 2268.550 -t 81.925 -qscale 0 video136.avi
ffmpeg -i audio136.avi -i video136.avi -acodec copy -vcodec copy chunk136.avi
ffmpeg -i ..\face-converted.avi -ss 2365.098 -t 11.652 -qscale 0 chunk137.avi
ffmpeg -i ..\face-converted.avi -ss 2440.296 -t 22.797 -vn -qscale 0 audio139.avi
ffmpeg -i ..\desktop-converted.avi -ss 2425.673 -t 22.797 -qscale 0 video139.avi
ffmpeg -i audio139.avi -i video139.avi -acodec copy -vcodec copy chunk139.avi
ffmpeg -i ..\face-converted.avi -ss 2472.819 -t 15.133 -vn -qscale 0 audio141.avi
ffmpeg -i ..\desktop-converted.avi -ss 2458.196 -t 15.133 -qscale 0 video141.avi
ffmpeg -i audio141.avi -i video141.avi -acodec copy -vcodec copy chunk141.avi
ffmpeg -i ..\face-converted.avi -ss 2487.952 -t 19.614 -vn -qscale 0 audio142.avi
ffmpeg -i ..\desktop-converted.avi -ss 2473.329 -t 19.614 -qscale 0 video142.avi
ffmpeg -i audio142.avi -i video142.avi -acodec copy -vcodec copy chunk142.avi
ffmpeg -i ..\face-converted.avi -ss 2507.566 -t 16.950 -qscale 0 chunk143.avi
ffmpeg -i ..\face-converted.avi -ss 2535.032 -t 23.187 -vn -qscale 0 audio145.avi
ffmpeg -i ..\desktop-converted.avi -ss 2520.409 -t 23.187 -qscale 0 video145.avi
ffmpeg -i audio145.avi -i video145.avi -acodec copy -vcodec copy chunk145.avi
ffmpeg -i ..\face-converted.avi -ss 2581.394 -t 2.056 -vn -qscale 0 audio147.avi
ffmpeg -i ..\desktop-converted.avi -ss 2566.771 -t 2.056 -qscale 0 video147.avi
ffmpeg -i audio147.avi -i video147.avi -acodec copy -vcodec copy chunk147.avi
ffmpeg -i ..\face-converted.avi -ss 2606.480 -t 3.739 -vn -qscale 0 audio149.avi
ffmpeg -i ..\desktop-converted.avi -ss 2591.857 -t 3.739 -qscale 0 video149.avi
ffmpeg -i audio149.avi -i video149.avi -acodec copy -vcodec copy chunk149.avi
ffmpeg -i ..\face-converted.avi -ss 2613.302 -t 5.702 -qscale 0 chunk151.avi
ffmpeg -i ..\face-converted.avi -ss 2636.019 -t 13.799 -qscale 0 chunk153.avi
cd ..
