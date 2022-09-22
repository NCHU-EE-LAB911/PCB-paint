
class transfer_rectangle():
    def transfer_zeros2one(self, matrix, max_row_number, max_column_number, MaxH, MaxW, max_row2height, max_area):
        M, N = len(matrix), len(matrix[0])
        current_row_number = 0
        pixel_position = str()
        
        
        for row in matrix:
            current_row_number = current_row_number + 1
        
            for i in range(N):
                # print("max_row_number, max_column_number, MaxW, current_row_number, = ", max_row_number, max_column_number, MaxW, current_row_number)
                if max_row2height[i] == max_area and ((max_row_number >= current_row_number > max_row_number - MaxW) \
                    or ((max_row_number == max_area) and max_row_number >= current_row_number)):
                    c = 0
                    # if i == 2:
                    #     print("上面的",max_row2height, max_row_number)                    
                elif (current_row_number >= max_row_number - MaxW + 1) and (current_row_number <= max_row_number) and ((i > max_column_number - MaxH - 1) \
                    and (i < max_column_number)) and MaxH != 1:
                    c = 0
                    # if i == 2:
                    #     print("BOTTON",max_row2height, max_row_number)  
                else:
                    c = row[i]
        

                if i < N - 1:
                    pixel_position = str(pixel_position) + str(c) + ','
                else:
                    pixel_position = str(pixel_position) + str(c)                
            pixel_position = str(pixel_position) + '\n'
            # print(pixel_position)
        f = open(r".\transfer_rectangle_matrix.txt", "w+")
        f.writelines(pixel_position)
        f.close
        return



    def store_rectangle(self, matrix, max_row_number, max_column_number, MaxH, MaxW, empty_pixel, max_row2height, max_area):
        M, N = len(matrix), len(matrix[0])

        current_row_number = 0
        pixel_position = str()
        
        for row in empty_pixel:
            current_row_number = current_row_number + 1
            
            for i in range(N):
                
                if max_row2height[i] == max_area and ((max_row_number >= current_row_number > max_row_number - MaxW) \
                    or ((max_row_number == max_area) and max_row_number >= current_row_number)):
                    # print("max_row_number, current_row_number, MaxW = ", max_row_number, current_row_number, MaxW)
                    c = 1
                    # if i == 1:
                    #     print("上面的", max_row_number, current_row_number, MaxW)
                elif (current_row_number >= max_row_number - MaxW + 1) and (current_row_number <= max_row_number) and ((i > max_column_number - MaxH - 1)\
                    and (i < max_column_number)) and MaxH != 1:
                    c = 1
                    # if i == 1:
                    #     print("BOTTON",max_row2height, max_row_number)  
                else:
                    c = row[i]
        
                if i < N - 1:
                    pixel_position = str(pixel_position) + str(c) + ','
                else:
                    pixel_position = str(pixel_position) + str(c)
            pixel_position = str(pixel_position) + '\n'
            # print(pixel_position)
        f = open(r".\store_rectangle_matrix.txt", "w+")
        f.writelines(pixel_position)
        f.close
        return    



