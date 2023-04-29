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

screens_names_train = listdir(screens_dir_train)                # список имен скриншотов в папке
screens_names_test = listdir(screens_dir_test)                  # список имен скриншотов в папке

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
# x_train.shape() = (n, height, width), где n - количество изображений в обучающей выборке
# y_train.shape() = (n, vertices_count * 3), где vertices_count*3 означает, что у каждой вершины 3 координаты
x_train = np.zeros((len(screens_paths_train), height, width), dtype=np.uint8)
y_train = np.zeros((len(screens_paths_train), vertices_count * 3), dtype=np.float32)
x_test = np.zeros((len(screens_paths_test), height, width), dtype=np.uint8)
y_test = np.zeros((len(screens_paths_test), vertices_count * 3), dtype=np.float32)

for i in range(len(screens_paths_train)):
    image = Image.open(screens_paths_train[i])                  # открываем изображение
    pix = image.load()                                          # выгружаем значения пикселей

    for x in range(height):
        for y in range(width):
            r, g, b = pix[y, x][0:3]                            # получаем параметры Red, Green, Blue из изображения
            grayscale_pixel = 0.299 * r + 0.587 * g + 0.114 * b # формула для получения оттенка серого
            x_train[i][x][y] = grayscale_pixel                  # записываем в i-ую картинку в пиксель с координатами (x,y) его оттенок серого

    image.close()                                               # закрываем изображение

    vertices_file_path = join(vertices_dir_train, (screens_names_train[i].split(".")[0]) + ".txt")
    vertices_file = open(vertices_file_path, "r")
    data = vertices_file.read()
    vertices_file.close()

    data_list = re.split(r', |\n', data)
    y_train[i] = np.asarray(data_list[:vertices_count*3])

for i in range(len(screens_paths_test)):
    image = Image.open(screens_paths_test[i])                   # открываем изображение
    pix = image.load()                                          # выгружаем значения пикселей

    for x in range(height):
        for y in range(width):
            r, g, b = pix[y, x][0:3]                            # получаем параметры Red, Green, Blue из изображения
            grayscale_pixel = 0.299 * r + 0.587 * g + 0.114 * b # формула для получения оттенка серого
            x_test[i][x][y] = grayscale_pixel                   # записываем в i-ую картинку в пиксель с координатами (x,y) его оттенок серого

    image.close()                                               # закрываем изображение

    vertices_file_path = join(vertices_dir_test, (screens_names_test[i].split(".")[0]) + ".txt")
    vertices_file = open(vertices_file_path, "r")
    data = vertices_file.read()
    vertices_file.close()

    data_list = re.split(r', |\n', data)
    y_test[i] = np.asarray(data_list[:48])

np.savez("dataset (1 image, 1 projection)", x_train, y_train, x_test, y_test)