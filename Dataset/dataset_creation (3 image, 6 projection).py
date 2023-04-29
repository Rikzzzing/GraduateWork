from PIL import Image, ImageDraw
import numpy as np
from os import listdir
from os.path import join
import matplotlib.pyplot as plt
import pathlib as pl
import re

# поиск путей до нужных папок
current_path = pl.Path(__file__).parent.resolve()               # директория, в которой расположен сам скрипт
screens_dir_train  = current_path.joinpath(r"x_train")          # директория обучающей выборки изображений
vertices_dir_train = current_path.joinpath(r"y_train")          # директория обучающей выборки вершин
screens_dir_test  = current_path.joinpath(r"x_test")            # директория проверочной выборки изображений
vertices_dir_test = current_path.joinpath(r"y_test")            # директория проверочной выборки вершин

screens_names_train_buf = listdir(screens_dir_train)            # список имен скриншотов в папке
screens_names_test_buf = listdir(screens_dir_test)              # список имен скриншотов в папке

screens_names_train = []
screens_names_test = []

for screen_name in screens_names_train_buf:
    if (screen_name.find('_X') != -1):
        screens_names_train.append(screen_name)

for screen_name in screens_names_test_buf:
    if (screen_name.find('_X') != -1):
        screens_names_test.append(screen_name)

screens_paths_train = []                                        # список путей к каждому скриншоту обучающей выборки (изначально пуст)
screens_paths_test = []                                         # список путей к каждому скриншоту проверочной выборки (изначально пуст)

for screen in screens_names_train:
    screens_paths_train.append(join(screens_dir_train, screen)) # заполнение списка путей к каждому скриншоту обучающей выборки

for screen in screens_names_test:
    screens_paths_test.append(join(screens_dir_test, screen))   # заполнение списка путей к каждому скриншоту проверочной выборки

# параметры файлов
image = Image.open(screens_paths_train[0])                      # открываем любой скриншот
width = image.size[0]                                           # определяем ширину
height = image.size[1]                                          # определяем высоту
image.close()                                                   # закрываем изображение

vertices_count = 16

# предварительно заполняем x_train, y_train, x_test, y_test нулями:
# x_train.shape() = (n, height*3, width), где n - количество изображений в обучающей выборке (height*3 означает, что у каждого объекта 3 картинки)
# y_train.shape() = (n, vertices_count * 3), где vertices_count*3 означает, что у каждой вершины 3 координаты
x_train = np.zeros((len(screens_paths_train), height * 3, width), dtype=np.uint8)
y_train = np.zeros((len(screens_paths_train), vertices_count * 3), dtype=np.float32)
x_test = np.zeros((len(screens_paths_test), height * 3, width), dtype=np.uint8)
y_test = np.zeros((len(screens_paths_test), vertices_count * 3), dtype=np.float32)

for i in range(len(screens_paths_train)):
    cur_path = screens_paths_train[i]
    image_x = Image.open(cur_path)                                      # открываем изображение X
    image_y = Image.open(cur_path.split("X")[0] + 'Y.png')              # открываем изображение Y
    image_z = Image.open(cur_path.split("X")[0] + 'Z.png')              # открываем изображение Z

    pix_x = image_x.load()                                              # выгружаем значения пикселей изображения X
    pix_y = image_y.load()                                              # выгружаем значения пикселей изображения y
    pix_z = image_z.load()                                              # выгружаем значения пикселей изображения z

    for x in range(height):
        for y in range(width):
            rx, gx, bx = pix_x[y, x][0:3]                               # получаем параметры Red, Green, Blue из изображения x
            ry, gy, by = pix_y[y, x][0:3]                               # получаем параметры Red, Green, Blue из изображения y
            rz, gz, bz = pix_z[y, x][0:3]                               # получаем параметры Red, Green, Blue из изображения z
            grayscale_pixel_x = 0.299 * rx + 0.587 * gx + 0.114 * bx    # формула для получения оттенка серого
            grayscale_pixel_y = 0.299 * ry + 0.587 * gy + 0.114 * by    # формула для получения оттенка серого
            grayscale_pixel_z = 0.299 * rz + 0.587 * gz + 0.114 * bz    # формула для получения оттенка серого
            x_train[i][x][y] = grayscale_pixel_x
            x_train[i][x + height][y] = grayscale_pixel_y
            x_train[i][x + height * 2][y] = grayscale_pixel_z

    image_x.close()                                                     # закрываем изображение
    image_y.close()                                                     # закрываем изображение
    image_z.close()                                                     # закрываем изображение

    vertices_file_path = join(vertices_dir_train, (screens_names_train[i].split("_X")[0]) + ".txt")
    vertices_file = open(vertices_file_path, "r")
    data = vertices_file.read()
    vertices_file.close()

    data_list = re.split(r', |\n', data)
    y_train[i] = np.asarray(data_list[:vertices_count*3])

for i in range(len(screens_paths_test)):
    cur_path = screens_paths_test[i]
    image_x = Image.open(cur_path)                                      # открываем изображение X
    image_y = Image.open(cur_path.split("X")[0] + 'Y.png')              # открываем изображение Y
    image_z = Image.open(cur_path.split("X")[0] + 'Z.png')              # открываем изображение Z

    pix_x = image_x.load()                                              # выгружаем значения пикселей изображения X
    pix_y = image_y.load()                                              # выгружаем значения пикселей изображения y
    pix_z = image_z.load()                                              # выгружаем значения пикселей изображения z

    for x in range(height):
        for y in range(width):
            rx, gx, bx = pix_x[y, x][0:3]                               # получаем параметры Red, Green, Blue из изображения x
            ry, gy, by = pix_y[y, x][0:3]                               # получаем параметры Red, Green, Blue из изображения y
            rz, gz, bz = pix_z[y, x][0:3]                               # получаем параметры Red, Green, Blue из изображения z
            grayscale_pixel_x = 0.299 * rx + 0.587 * gx + 0.114 * bx    # формула для получения оттенка серого
            grayscale_pixel_y = 0.299 * ry + 0.587 * gy + 0.114 * by    # формула для получения оттенка серого
            grayscale_pixel_z = 0.299 * rz + 0.587 * gz + 0.114 * bz    # формула для получения оттенка серого
            x_test[i][x][y] = grayscale_pixel_x
            x_test[i][x + height][y] = grayscale_pixel_y
            x_test[i][x + height * 2][y] = grayscale_pixel_z

    image_x.close()                                                     # закрываем изображение
    image_y.close()                                                     # закрываем изображение
    image_z.close()                                                     # закрываем изображение

    vertices_file_path = join(vertices_dir_test, (screens_names_test[i].split("_X")[0]) + ".txt")
    vertices_file = open(vertices_file_path, "r")
    data = vertices_file.read()
    vertices_file.close()

    data_list = re.split(r', |\n', data)
    y_test[i] = np.asarray(data_list[:48])

np.savez("dataset (3 image, 6 projection)", x_train, y_train, x_test, y_test)