from number_rectangle import *
from transfer_pixel import *
from extract_image_color import *
from store_eachpixel_rgb2txt import *

if __name__ == '__main__':

    '''提取圖片塗膠區,並resize圖片'''
    extract_image_color1.select_image()
    #resize的比例
    f = open('1.txt','r')
    resize_magnification = f.read().replace('\n', ' ')
    f.close()

    '''將提取完的圖片轉換成txt'''
    store_eachPixel_rgb2txt().store_eachPixel_rgb2txt()

    '''這邊是給matrix初始值'''
    # 讀txt內的矩陣 def內的資料格式是 1.type matrix: List[List[str]]、2.rtype: int
    matrix=[]
    f = open('pixel_position_test.txt','r')
    for line in f:
        matrix.append(list(line.strip('\n').split(',')))
    f.close()
    
    #創建空的txt，儲存被覆蓋的矩形位置
    M, N = len(matrix), len(matrix[0])
    empty_pixel = str()
    for i in range(M):
        for j in range(N):
            if j < N:
                empty_pixel = str(empty_pixel) + str(0) + ","
            else:
                empty_pixel = str(empty_pixel) + str(0)
        empty_pixel = str(empty_pixel) + '\n'
    f = open(r".\store_rectangle_matrix.txt", "w+")
    f.writelines(empty_pixel)
    f.close
    
    empty_pixel=[]
    f = open('store_rectangle_matrix.txt','r')
    for line in f:
        empty_pixel.append(list(line.strip('\n').split(',')))
    f.close()

    #儲存矩形四個角的座標
    store_rectangle_edge = str()

    '''這邊開始找第一個矩形'''
    
    #找出最大矩形
    maxrectangle = Solution().maximalRectangle(matrix)
    max_area = maxrectangle[0]
    max_row_number = maxrectangle[1]
    max_column_number = maxrectangle[2]
    MaxH = maxrectangle[3]
    MaxW = maxrectangle[4]
    max_row2height = maxrectangle[5]
    
    #覆蓋最大矩形
    cover_rectangle = transfer_rectangle().transfer_zeros2one(matrix, max_row_number, max_column_number, MaxH, MaxW, max_row2height, max_area)

    #將最大矩形紀錄到新的txt上
    biggest_rectangle = transfer_rectangle().store_rectangle(matrix, max_row_number, max_column_number, MaxH, MaxW, empty_pixel, max_row2height, max_area)

    #儲存矩形四個角的座標
    top_left = str(max_column_number - MaxH + 1) + ',' + str(max_row_number - MaxW + 1)
    top_right = ',' + str(max_column_number) + ',' + str(max_row_number - MaxW + 1)
    bottom_right = ',' + str(max_column_number + 1) + ',' + str(max_row_number + 1)   # ","記得加
    bottom_left = ',' + str(max_column_number - MaxH + 1) + ',' + str(max_row_number)
    # store_rectangle_edge = str(resize_magnification) + '\n' + top_left + top_right + bottom_right + bottom_left
    print("resize magnification = ", resize_magnification)
    store_rectangle_edge = resize_magnification + '\n' + top_left + bottom_right
    print("max_area = ", max_area)

    '''這邊用while找後面矩形'''

    while max_area > 10:
            matrix=[]
            f = open('transfer_rectangle_matrix.txt','r')
            for line in f:
                matrix.append(list(line.strip('\n').split(',')))
            f.close()
            
            #找出最大矩形
            maxrectangle = Solution().maximalRectangle(matrix)
            max_area = maxrectangle[0]
            max_row_number = maxrectangle[1]
            max_column_number = maxrectangle[2]
            MaxH = maxrectangle[3]
            MaxW = maxrectangle[4]
            max_row2height = maxrectangle[5]

            #儲存矩形四個角的座標
            top_left = str(max_column_number - MaxH + 1) + ',' + str(max_row_number - MaxW + 1)
            top_right = ',' + str(max_column_number) + ',' + str(max_row_number - MaxW + 1)
            bottom_right = ',' + str(max_column_number + 1) + ',' + str(max_row_number + 1)   # ","記得加
            bottom_left = ',' + str(max_column_number - MaxH + 1) + ',' + str(max_row_number)
            # store_rectangle_edge = store_rectangle_edge + '\n' + top_left + top_right + bottom_right + bottom_left
            store_rectangle_edge = str(store_rectangle_edge) + '\n' + top_left + bottom_right
            print("max_area = ", max_area)

            #覆蓋最大矩形
            cover_rectangle = transfer_rectangle().transfer_zeros2one(matrix, max_row_number, max_column_number, MaxH, MaxW, max_row2height, max_area)

            #將最大矩形紀錄到新的txt上
            empty_pixel=[]
            f = open('store_rectangle_matrix.txt','r')
            for line in f:
                empty_pixel.append(list(line.strip('\n').split(',')))
            f.close()
            biggest_rectangle = transfer_rectangle().store_rectangle(matrix, max_row_number, max_column_number, MaxH, MaxW, empty_pixel, max_row2height, max_area)
            
    print("max_area = ", max_area)
    # store_rectangle_edge = store_rectangle_edge + '\n' + store_rectangle_edge
    c = str(1) + ".txt"
    f = open(c, 'w+')
    f.writelines(store_rectangle_edge)
    f.close
    
 