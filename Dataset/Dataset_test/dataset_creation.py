from PIL import Image, ImageDraw
import numpy as np
from os import listdir
from os.path import join
import matplotlib.pyplot as plt
import pathlib as pl
import re


# screens_dir_train  = r'C:\Other_projects\Python\NNetwork\Dataset_test\x_train' # путь к папке со скриншотами
# vertices_dir_train = r'C:\Other_projects\Python\NNetwork\Dataset_test\y_train' # путь к папке с вершинами
# current_path = r"E:\Unity\DiplomRaycast\Dataset"
current_path = pl.Path(__file__).parent.resolve()
screens_dir_train  = current_path.joinpath(r"x_train")
vertices_dir_train = current_path.joinpath(r"y_train")
screens_dir_test  = current_path.joinpath(r"x_test")
vertices_dir_test = current_path.joinpath(r"y_test")

screens_names_train = listdir(screens_dir_train) # список скриншотов в папке
screens_names_test = listdir(screens_dir_test) # список скриншотов в папке

screens_paths_train = [] # список путей к каждому скриншоту
screens_paths_test = [] # список путей к каждому скриншоту

for screen in screens_names_train:
    screens_paths_train.append(join(screens_dir_train, screen))

for screen in screens_names_test:
    screens_paths_test.append(join(screens_dir_test, screen))

# for path in screens_paths_train:
#     print(path)
# exit()

# параметры файлов
width = 512
height = 512
vertices_count = 16

x_train = np.zeros((len(screens_paths_train), width, height), dtype=np.uint8)
y_train = np.zeros((len(screens_paths_train), vertices_count * 3), dtype=np.float32)
x_test = np.zeros((len(screens_paths_test), width, height), dtype=np.uint8)
y_test = np.zeros((len(screens_paths_test), vertices_count * 3), dtype=np.float32)
# print("--X_train creation success--")
# print(len(screens_paths_train))
# exit()

for i in range(len(screens_paths_train)):
    image = Image.open(screens_paths_train[i])    # Открываем изображение
    # width = image.size[0]                 # Определяем ширину
    # height = image.size[1]                # Определяем высоту
    pix = image.load()                      # Выгружаем значения пикселей

    for x in range(width):
        for y in range(height):
            # cur_pix = image.getpixel((x, y))
            # r, g, b = cur_pix
            # print(pix[x, y][0:3])
            r, g, b = pix[x, y][0:3]
            grayscale_pixel = 0.299 * r + 0.587 * g + 0.114 * b
            x_train[i][y][x] = grayscale_pixel

    image.close()

    vertices_file_path = join(vertices_dir_train, (screens_names_train[i].split(".")[0]) + ".txt")
    vertices_file = open(vertices_file_path, "r")
    data = vertices_file.read()
    vertices_file.close()

    data_list = re.split(r', |\n', data)
    y_train[i] = np.asarray(data_list[:48])

for i in range(len(screens_paths_test)):
    image = Image.open(screens_paths_test[i])    # Открываем изображение
    # width = image.size[0]                 # Определяем ширину
    # height = image.size[1]                # Определяем высоту
    pix = image.load()                      # Выгружаем значения пикселей

    for x in range(width):
        for y in range(height):
            # cur_pix = image.getpixel((x, y))
            # r, g, b = cur_pix
            # print(pix[x, y][0:3])
            r, g, b = pix[x, y][0:3]
            grayscale_pixel = 0.299 * r + 0.587 * g + 0.114 * b
            x_test[i][y][x] = grayscale_pixel

    image.close()

    vertices_file_path = join(vertices_dir_test, (screens_names_test[i].split(".")[0]) + ".txt")
    vertices_file = open(vertices_file_path, "r")
    data = vertices_file.read()
    vertices_file.close()

    data_list = re.split(r', |\n', data)
    y_test[i] = np.asarray(data_list[:48])

# print("========== result ==========")
# for i in range(len(screens_paths_train)):
#     print("x_train[i]:")
#     print(x_train[i])
#     print("y_train[i]:")
#     print(y_train[i])


np.savez("dataset", x_train, y_train, x_test, y_test)

# plt.imshow( x_train[0])
# plt.show()