rmdir /s /q chunks
mkdir chunks
cd chunks
ffmpeg -i ..\face-converted.avi -ss 14.623 -t 23.466 -acodec copy -vcodec copy chunk002.avi
ffmpeg -i ..\face-converted.avi -ss 47.567 -t 8.111 -acodec copy -vcodec copy chunk004.avi
ffmpeg -i ..\face-converted.avi -ss 98.988 -t 5.407 -acodec copy -vcodec copy chunk006.avi
ffmpeg -i ..\face-converted.avi -ss 117.882 -t 13.186 -acodec copy -vcodec copy chunk008.avi
ffmpeg -i ..\face-converted.avi -ss 137.614 -t 1.895 -acodec copy -vcodec copy chunk010.avi
ffmpeg -i ..\face-converted.avi -ss 143.381 -t 8.143 -acodec copy -vcodec copy chunk012.avi
ffmpeg -i ..\face-converted.avi -ss 164.993 -t 6.494 -acodec copy -vcodec copy chunk014.avi
ffmpeg -i ..\face-converted.avi -ss 175.301 -t 4.272 -acodec copy -vcodec copy chunk016.avi
ffmpeg -i ..\face-converted.avi -ss 190.456 -t 26.091 -acodec copy -vcodec copy chunk018.avi
ffmpeg -i ..\face-converted.avi -ss 227.369 -t 17.788 -acodec copy -vcodec copy chunk020.avi
ffmpeg -i ..\face-converted.avi -ss 284.061 -t 13.327 -acodec copy -vcodec copy chunk022.avi
ffmpeg -i ..\face-converted.avi -ss 303.685 -t 31.977 -acodec copy -vcodec copy chunk024.avi
ffmpeg -i ..\face-converted.avi -ss 350.094 -t 2.627 -acodec copy -vn audio026.avi
ffmpeg -i ..\desktop-converted.avi -ss 335.471 -t 2.627 -acodec copy -vcodec copy video026.avi
ffmpeg -i audio026.avi -i video026.avi -acodec copy -vcodec copy chunk026.avi
ffmpeg -i ..\face-converted.avi -ss 372.476 -t 2.781 -acodec copy -vcodec copy chunk028.avi
ffmpeg -i ..\face-converted.avi -ss 412.588 -t 1.985 -acodec copy -vcodec copy chunk030.avi
ffmpeg -i ..\face-converted.avi -ss 485.399 -t 2.787 -acodec copy -vn audio032.avi
ffmpeg -i ..\desktop-converted.avi -ss 470.776 -t 2.787 -acodec copy -vcodec copy video032.avi
ffmpeg -i audio032.avi -i video032.avi -acodec copy -vcodec copy chunk032.avi
ffmpeg -i ..\face-converted.avi -ss 497.365 -t 3.508 -acodec copy -vn audio034.avi
ffmpeg -i ..\desktop-converted.avi -ss 482.742 -t 3.508 -acodec copy -vcodec copy video034.avi
ffmpeg -i audio034.avi -i video034.avi -acodec copy -vcodec copy chunk034.avi
ffmpeg -i ..\face-converted.avi -ss 505.162 -t 4.751 -acodec copy -vcodec copy chunk036.avi
ffmpeg -i ..\face-converted.avi -ss 518.319 -t 5.545 -acodec copy -vcodec copy chunk038.avi
ffmpeg -i ..\face-converted.avi -ss 533.973 -t 2.101 -acodec copy -vcodec copy chunk040.avi
ffmpeg -i ..\face-converted.avi -ss 536.074 -t 8.259 -acodec copy -vcodec copy chunk041.avi
ffmpeg -i ..\face-converted.avi -ss 544.333 -t 3.384 -acodec copy -vn audio042.avi
ffmpeg -i ..\desktop-converted.avi -ss 529.710 -t 3.384 -acodec copy -vcodec copy video042.avi
ffmpeg -i audio042.avi -i video042.avi -acodec copy -vcodec copy chunk042.avi
ffmpeg -i ..\face-converted.avi -ss 558.776 -t 7.257 -acodec copy -vcodec copy chunk044.avi
ffmpeg -i ..\face-converted.avi -ss 566.033 -t 34.069 -acodec copy -vn audio045.avi
ffmpeg -i ..\desktop-converted.avi -ss 551.410 -t 34.069 -acodec copy -vcodec copy video045.avi
ffmpeg -i audio045.avi -i video045.avi -acodec copy -vcodec copy chunk045.avi
ffmpeg -i ..\face-converted.avi -ss 600.102 -t 11.601 -acodec copy -vcodec copy chunk046.avi
ffmpeg -i ..\face-converted.avi -ss 622.327 -t 10.353 -acodec copy -vn audio048.avi
ffmpeg -i ..\desktop-converted.avi -ss 607.704 -t 10.353 -acodec copy -vcodec copy video048.avi
ffmpeg -i audio048.avi -i video048.avi -acodec copy -vcodec copy chunk048.avi
ffmpeg -i ..\face-converted.avi -ss 652.885 -t 12.045 -acodec copy -vcodec copy chunk050.avi
ffmpeg -i ..\face-converted.avi -ss 680.692 -t 8.037 -acodec copy -vcodec copy chunk052.avi
ffmpeg -i ..\face-converted.avi -ss 688.729 -t 10.126 -acodec copy -vcodec copy chunk053.avi
ffmpeg -i ..\face-converted.avi -ss 712.258 -t 4.547 -acodec copy -vn audio055.avi
ffmpeg -i ..\desktop-converted.avi -ss 697.635 -t 4.547 -acodec copy -vcodec copy video055.avi
ffmpeg -i audio055.avi -i video055.avi -acodec copy -vcodec copy chunk055.avi
ffmpeg -i ..\face-converted.avi -ss 721.155 -t 12.282 -acodec copy -vn audio057.avi
ffmpeg -i ..\desktop-converted.avi -ss 706.532 -t 12.282 -acodec copy -vcodec copy video057.avi
ffmpeg -i audio057.avi -i video057.avi -acodec copy -vcodec copy chunk057.avi
ffmpeg -i ..\face-converted.avi -ss 746.757 -t 5.514 -acodec copy -vn audio059.avi
ffmpeg -i ..\desktop-converted.avi -ss 732.134 -t 5.514 -acodec copy -vcodec copy video059.avi
ffmpeg -i audio059.avi -i video059.avi -acodec copy -vcodec copy chunk059.avi
ffmpeg -i ..\face-converted.avi -ss 760.995 -t 21.956 -acodec copy -vn audio061.avi
ffmpeg -i ..\desktop-converted.avi -ss 746.372 -t 21.956 -acodec copy -vcodec copy video061.avi
ffmpeg -i audio061.avi -i video061.avi -acodec copy -vcodec copy chunk061.avi
ffmpeg -i ..\face-converted.avi -ss 790.487 -t 5.574 -acodec copy -vn audio063.avi
ffmpeg -i ..\desktop-converted.avi -ss 775.864 -t 5.574 -acodec copy -vcodec copy video063.avi
ffmpeg -i audio063.avi -i video063.avi -acodec copy -vcodec copy chunk063.avi
ffmpeg -i ..\face-converted.avi -ss 856.582 -t 10.372 -acodec copy -vn audio065.avi
ffmpeg -i ..\desktop-converted.avi -ss 841.959 -t 10.372 -acodec copy -vcodec copy video065.avi
ffmpeg -i audio065.avi -i video065.avi -acodec copy -vcodec copy chunk065.avi
ffmpeg -i ..\face-converted.avi -ss 896.334 -t 7.185 -acodec copy -vn audio067.avi
ffmpeg -i ..\desktop-converted.avi -ss 881.711 -t 7.185 -acodec copy -vcodec copy video067.avi
ffmpeg -i audio067.avi -i video067.avi -acodec copy -vcodec copy chunk067.avi
ffmpeg -i ..\face-converted.avi -ss 908.800 -t 5.104 -acodec copy -vn audio069.avi
ffmpeg -i ..\desktop-converted.avi -ss 894.177 -t 5.104 -acodec copy -vcodec copy video069.avi
ffmpeg -i audio069.avi -i video069.avi -acodec copy -vcodec copy chunk069.avi
ffmpeg -i ..\face-converted.avi -ss 966.301 -t 14.863 -acodec copy -vcodec copy chunk071.avi
ffmpeg -i ..\face-converted.avi -ss 987.442 -t 4.289 -acodec copy -vcodec copy chunk073.avi
ffmpeg -i ..\face-converted.avi -ss 997.879 -t 40.006 -acodec copy -vcodec copy chunk075.avi
ffmpeg -i ..\face-converted.avi -ss 1037.885 -t 0.000 -acodec copy -vcodec copy chunk076.avi
ffmpeg -i ..\face-converted.avi -ss 1054.358 -t 22.384 -acodec copy -vcodec copy chunk078.avi
ffmpeg -i ..\face-converted.avi -ss 1096.132 -t 17.495 -acodec copy -vcodec copy chunk080.avi
ffmpeg -i ..\face-converted.avi -ss 1113.627 -t 18.958 -acodec copy -vcodec copy chunk081.avi
ffmpeg -i ..\face-converted.avi -ss 1132.585 -t 32.576 -acodec copy -vn audio082.avi
ffmpeg -i ..\desktop-converted.avi -ss 1117.962 -t 32.576 -acodec copy -vcodec copy video082.avi
ffmpeg -i audio082.avi -i video082.avi -acodec copy -vcodec copy chunk082.avi
ffmpeg -i ..\face-converted.avi -ss 1165.161 -t 7.980 -acodec copy -vcodec copy chunk083.avi
ffmpeg -i ..\face-converted.avi -ss 1180.858 -t 15.867 -acodec copy -vn audio085.avi
ffmpeg -i ..\desktop-converted.avi -ss 1166.235 -t 15.867 -acodec copy -vcodec copy video085.avi
ffmpeg -i audio085.avi -i video085.avi -acodec copy -vcodec copy chunk085.avi
ffmpeg -i ..\face-converted.avi -ss 1204.108 -t 11.452 -acodec copy -vcodec copy chunk087.avi
ffmpeg -i ..\face-converted.avi -ss 1222.231 -t 3.571 -acodec copy -vcodec copy chunk089.avi
ffmpeg -i ..\face-converted.avi -ss 1263.812 -t 15.306 -acodec copy -vcodec copy chunk091.avi
ffmpeg -i ..\face-converted.avi -ss 1297.340 -t 4.248 -acodec copy -vcodec copy chunk093.avi
ffmpeg -i ..\face-converted.avi -ss 1357.388 -t 48.127 -acodec copy -vcodec copy chunk095.avi
ffmpeg -i ..\face-converted.avi -ss 1405.515 -t 8.032 -acodec copy -vn audio096.avi
ffmpeg -i ..\desktop-converted.avi -ss 1390.892 -t 8.032 -acodec copy -vcodec copy video096.avi
ffmpeg -i audio096.avi -i video096.avi -acodec copy -vcodec copy chunk096.avi
ffmpeg -i ..\face-converted.avi -ss 1441.592 -t 3.333 -acodec copy -vcodec copy chunk098.avi
ffmpeg -i ..\face-converted.avi -ss 1449.619 -t 16.043 -acodec copy -vcodec copy chunk100.avi
ffmpeg -i ..\face-converted.avi -ss 1483.413 -t 18.176 -acodec copy -vn audio102.avi
ffmpeg -i ..\desktop-converted.avi -ss 1468.790 -t 18.176 -acodec copy -vcodec copy video102.avi
ffmpeg -i audio102.avi -i video102.avi -acodec copy -vcodec copy chunk102.avi
ffmpeg -i ..\face-converted.avi -ss 1518.073 -t 3.793 -acodec copy -vn audio104.avi
ffmpeg -i ..\desktop-converted.avi -ss 1503.450 -t 3.793 -acodec copy -vcodec copy video104.avi
ffmpeg -i audio104.avi -i video104.avi -acodec copy -vcodec copy chunk104.avi
ffmpeg -i ..\face-converted.avi -ss 1570.690 -t 15.210 -acodec copy -vn audio106.avi
ffmpeg -i ..\desktop-converted.avi -ss 1556.067 -t 15.210 -acodec copy -vcodec copy video106.avi
ffmpeg -i audio106.avi -i video106.avi -acodec copy -vcodec copy chunk106.avi
ffmpeg -i ..\face-converted.avi -ss 1585.900 -t 71.115 -acodec copy -vcodec copy chunk107.avi
ffmpeg -i ..\face-converted.avi -ss 1661.643 -t 22.444 -acodec copy -vn audio109.avi
ffmpeg -i ..\desktop-converted.avi -ss 1647.020 -t 22.444 -acodec copy -vcodec copy video109.avi
ffmpeg -i audio109.avi -i video109.avi -acodec copy -vcodec copy chunk109.avi
ffmpeg -i ..\face-converted.avi -ss 1733.411 -t 2.977 -acodec copy -vn audio111.avi
ffmpeg -i ..\desktop-converted.avi -ss 1718.788 -t 2.977 -acodec copy -vcodec copy video111.avi
ffmpeg -i audio111.avi -i video111.avi -acodec copy -vcodec copy chunk111.avi
ffmpeg -i ..\face-converted.avi -ss 1794.813 -t 3.121 -acodec copy -vn audio113.avi
ffmpeg -i ..\desktop-converted.avi -ss 1780.190 -t 3.121 -acodec copy -vcodec copy video113.avi
ffmpeg -i audio113.avi -i video113.avi -acodec copy -vcodec copy chunk113.avi
ffmpeg -i ..\face-converted.avi -ss 1810.117 -t 1.328 -acodec copy -vn audio115.avi
ffmpeg -i ..\desktop-converted.avi -ss 1795.494 -t 1.328 -acodec copy -vcodec copy video115.avi
ffmpeg -i audio115.avi -i video115.avi -acodec copy -vcodec copy chunk115.avi
ffmpeg -i ..\face-converted.avi -ss 1824.760 -t 9.072 -acodec copy -vn audio117.avi
ffmpeg -i ..\desktop-converted.avi -ss 1810.137 -t 9.072 -acodec copy -vcodec copy video117.avi
ffmpeg -i audio117.avi -i video117.avi -acodec copy -vcodec copy chunk117.avi
ffmpeg -i ..\face-converted.avi -ss 1853.455 -t 8.467 -acodec copy -vn audio119.avi
ffmpeg -i ..\desktop-converted.avi -ss 1838.832 -t 8.467 -acodec copy -vcodec copy video119.avi
ffmpeg -i audio119.avi -i video119.avi -acodec copy -vcodec copy chunk119.avi
ffmpeg -i ..\face-converted.avi -ss 1869.013 -t 3.511 -acodec copy -vn audio121.avi
ffmpeg -i ..\desktop-converted.avi -ss 1854.390 -t 3.511 -acodec copy -vcodec copy video121.avi
ffmpeg -i audio121.avi -i video121.avi -acodec copy -vcodec copy chunk121.avi
ffmpeg -i ..\face-converted.avi -ss 1894.772 -t 3.670 -acodec copy -vn audio123.avi
ffmpeg -i ..\desktop-converted.avi -ss 1880.149 -t 3.670 -acodec copy -vcodec copy video123.avi
ffmpeg -i audio123.avi -i video123.avi -acodec copy -vcodec copy chunk123.avi
ffmpeg -i ..\face-converted.avi -ss 1957.519 -t 34.403 -acodec copy -vcodec copy chunk125.avi
ffmpeg -i ..\face-converted.avi -ss 2019.134 -t 54.228 -acodec copy -vcodec copy chunk127.avi
ffmpeg -i ..\face-converted.avi -ss 2073.362 -t 0.000 -acodec copy -vcodec copy chunk128.avi
ffmpeg -i ..\face-converted.avi -ss 2087.424 -t 48.281 -acodec copy -vcodec copy chunk130.avi
ffmpeg -i ..\face-converted.avi -ss 2208.744 -t 13.179 -acodec copy -vcodec copy chunk132.avi
ffmpeg -i ..\face-converted.avi -ss 2221.923 -t 50.898 -acodec copy -vcodec copy chunk133.avi
ffmpeg -i ..\face-converted.avi -ss 2275.517 -t 7.656 -acodec copy -vn audio135.avi
ffmpeg -i ..\desktop-converted.avi -ss 2260.894 -t 7.656 -acodec copy -vcodec copy video135.avi
ffmpeg -i audio135.avi -i video135.avi -acodec copy -vcodec copy chunk135.avi
ffmpeg -i ..\face-converted.avi -ss 2283.173 -t 81.925 -acodec copy -vn audio136.avi
ffmpeg -i ..\desktop-converted.avi -ss 2268.550 -t 81.925 -acodec copy -vcodec copy video136.avi
ffmpeg -i audio136.avi -i video136.avi -acodec copy -vcodec copy chunk136.avi
ffmpeg -i ..\face-converted.avi -ss 2365.098 -t 11.652 -acodec copy -vcodec copy chunk137.avi
ffmpeg -i ..\face-converted.avi -ss 2440.296 -t 22.797 -acodec copy -vn audio139.avi
ffmpeg -i ..\desktop-converted.avi -ss 2425.673 -t 22.797 -acodec copy -vcodec copy video139.avi
ffmpeg -i audio139.avi -i video139.avi -acodec copy -vcodec copy chunk139.avi
ffmpeg -i ..\face-converted.avi -ss 2472.819 -t 15.133 -acodec copy -vn audio141.avi
ffmpeg -i ..\desktop-converted.avi -ss 2458.196 -t 15.133 -acodec copy -vcodec copy video141.avi
ffmpeg -i audio141.avi -i video141.avi -acodec copy -vcodec copy chunk141.avi
ffmpeg -i ..\face-converted.avi -ss 2487.952 -t 19.614 -acodec copy -vn audio142.avi
ffmpeg -i ..\desktop-converted.avi -ss 2473.329 -t 19.614 -acodec copy -vcodec copy video142.avi
ffmpeg -i audio142.avi -i video142.avi -acodec copy -vcodec copy chunk142.avi
ffmpeg -i ..\face-converted.avi -ss 2507.566 -t 16.950 -acodec copy -vcodec copy chunk143.avi
ffmpeg -i ..\face-converted.avi -ss 2535.032 -t 23.187 -acodec copy -vn audio145.avi
ffmpeg -i ..\desktop-converted.avi -ss 2520.409 -t 23.187 -acodec copy -vcodec copy video145.avi
ffmpeg -i audio145.avi -i video145.avi -acodec copy -vcodec copy chunk145.avi
ffmpeg -i ..\face-converted.avi -ss 2581.394 -t 2.056 -acodec copy -vn audio147.avi
ffmpeg -i ..\desktop-converted.avi -ss 2566.771 -t 2.056 -acodec copy -vcodec copy video147.avi
ffmpeg -i audio147.avi -i video147.avi -acodec copy -vcodec copy chunk147.avi
ffmpeg -i ..\face-converted.avi -ss 2606.480 -t 3.739 -acodec copy -vn audio149.avi
ffmpeg -i ..\desktop-converted.avi -ss 2591.857 -t 3.739 -acodec copy -vcodec copy video149.avi
ffmpeg -i audio149.avi -i video149.avi -acodec copy -vcodec copy chunk149.avi
ffmpeg -i ..\face-converted.avi -ss 2613.302 -t 5.702 -acodec copy -vcodec copy chunk151.avi
ffmpeg -i ..\face-converted.avi -ss 2636.019 -t 13.799 -acodec copy -vcodec copy chunk153.avi
cd ..
