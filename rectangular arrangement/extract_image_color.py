import cv2
import numpy as np
import tkinter as tk
from tkinter import filedialog

class extract_image_color1():
    
    def extract_image_color():
        
        #初始圖片縮小倍率
        resize_magnification = float(1)

        #選圖片
        sfname = filedialog.askopenfilename(title='選擇',
                                            filetypes=[
                                                ('All Files','*'),
                                                ("jpeg files","*.jpg"),
                                                ("png files","*.png"),
                                                ("gif files","*.gif")])
        img = cv2.imdecode(np.fromfile(sfname,dtype=np.uint8),-1)

        #圖片縮小，先縮小再提取塗膠區↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        # height = img.shape[0]
        # width = img.shape[1]
        # area = height * width
        # while area > 125000:
        #     img = cv2.resize(img, (int(width * 0.8), int(height * 0.8)))
        #     height = img.shape[0]
        #     width = img.shape[1]
        #     area = height * width
        #     resize_magnification = float(resize_magnification * 0.8)
        # resize_magnification = str(resize_magnification)
        # f = open(r".\1.txt", "w+")
        # f.writelines(resize_magnification)
        # f.close
        #圖片縮小，先縮小再提取塗膠區↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

        #轉HSV，提取塗膠區
        hsv = cv2.cvtColor(img, cv2.COLOR_BGR2HSV)
        low_blue = np.array([78,43,46])
        high_blue = np.array([124,255,255])
        mask = cv2.inRange(hsv,lowerb=low_blue,upperb=high_blue)

        #圖片縮小，先提取再縮小↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        height = mask.shape[0]
        width = mask.shape[1]
        area = height * width
        while area > 125000:
            mask = cv2.resize(mask, (int(width * 0.8), int(height * 0.8)))
            height = mask.shape[0]
            width = mask.shape[1]
            area = height * width
            resize_magnification = float(resize_magnification * 0.8)
        resize_magnification = str(resize_magnification)
        f = open(r".\1.txt", "w+")
        f.writelines(resize_magnification)
        f.close
        #圖片縮小，先提取再縮小↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
            
        cv2.imshow("test2", mask)
        cv2.imwrite("standard_extract.jpg", mask)
        cv2.waitKey(0)
        cv2.destroyAllWindows()
        return 

    def select_image():
        select_monitor = tk.Tk()
        B1 = tk.Button(select_monitor, text="打開", command = extract_image_color1.extract_image_color).pack()
        select_monitor.mainloop()
        return
