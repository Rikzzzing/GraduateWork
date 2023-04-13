import sys
import numpy
import matplotlib
import tensorflow
import keras

from matplotlib import pyplot

from keras import Sequential
from keras.utils import np_utils
from keras.layers import Conv2D, Dense, Dropout, Activation, Flatten
from keras.layers import Convolution2D, MaxPooling2D
from keras.datasets import mnist

# проверка среды и версий
# print(sys.version)
# print(sys.base_prefix)
# print('numpy:' + numpy.__version__)
# print('tensorflow:' + tensorflow.__version__)
# print('keras:' + keras.__version__)
# print('matplotlib:' + matplotlib.__version__)



# создание модели
model = Sequential()
model.add(Conv2D(filters = 32, kernel_size = (8, 8), activation = 'relu', input_shape = (512, 512, 1))) # картинка 512x512

# print(model.output_shape)
model.add(Conv2D(32, (6, 6), activation = 'relu'))
model.add(MaxPooling2D(pool_size = (5, 5)))
model.add(Dropout(0.25))

model.add(Flatten())

model.add(Dense(256, activation = 'relu'))
model.add(Dropout(0.5))
model.add(Dense(144, activation = 'softmax')) # 144=48*3 - т.е. получаем 48 вершин в 3d

# компиляция модели с оптимизатором Adam
model.compile(loss = 'categorical_crossentropy', optimizer = 'Adam', metrics = ['accuracy'])