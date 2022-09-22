from PIL import Image

class store_eachPixel_rgb2txt():
    def store_eachPixel_rgb2txt(self):
        
        '''將提取完的圖片轉換成txt'''
        # 定義圖片縮放大小
        img = Image.open('.\standard_extract.jpg')
        a, b = img.size

        # 讀pixel
        position_list = str()
        pixel = str()
        for i in range(b):
            
            for j in range(a):
                # if j == a - 1:
                #     print(i)
                pixel = img.getpixel((j,i))
                # print("pixel = ", pixel)
                if pixel > 2:
                    c = 1
                else: 
                    c = 0
                if j < a - 1:    
                    position_list = str(position_list) + str(c) + ',' #+ str(pixel[1]) + str(pixel[2])
                else: position_list = str(position_list) + str(c) #+ str(pixel[1]) + str(pixel[2])
            if i < b - 1:
                position_list = str(position_list) + '\n'


        f = open(r".\pixel_position_test.txt", "w+")
        f.writelines(position_list)
        f.close
        #f.writelines(x_str + ' ' + y_str + '\n')
        
        return


# # 定义图片放缩大小
# img = Image.open('.\standard_extract.jpg')
# a, b = img.size

# # 讀pixel
# position_list = str()
# pixel = str()
# for i in range(b):
    
#     for j in range(a):
#         if j == a - 1:
#             print(i)
#         pixel = img.getpixel((j,i))
#         # print("pixel = ", pixel)
#         if pixel > 2:
#             c = 1
#         else: 
#             c = 0
#         if j < a - 1:    
#             position_list = str(position_list) + str(c) + ',' #+ str(pixel[1]) + str(pixel[2])
#         else: position_list = str(position_list) + str(c) #+ str(pixel[1]) + str(pixel[2])
#     if i < b - 1:
#         position_list = str(position_list) + '\n'


# f = open(r".\pixel_position_test.txt", "w+")
# f.writelines(position_list)
# f.close
# #f.writelines(x_str + ' ' + y_str + '\n')



