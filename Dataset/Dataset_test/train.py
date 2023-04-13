import sys
import numpy as np
import matplotlib
import tensorflow
import keras

from matplotlib import pyplot

from keras import Sequential
from keras.utils import np_utils
from keras.layers import Conv2D, Dense, Dropout, Activation, Flatten
from keras.layers import Convolution2D, MaxPooling2D
# from keras.datasets import mnist

# проверка среды и версий
# print(sys.version)
# print(sys.base_prefix)
# print('numpy:' + numpy.__version__)
# print('tensorflow:' + tensorflow.__version__)
# print('keras:' + keras.__version__)
# print('matplotlib:' + matplotlib.__version__)

# ус2тановка ключа для генерации случайных чисел
np.random.seed(123)

# загрузка набора данных для обучения
arrays = np.load("dataset.npz")
for k in arrays.files:
    print(k)
X_train = arrays.f.arr_0
Y_train = arrays.f.arr_1
X_test = arrays.f.arr_2
Y_test = arrays.f.arr_3
# (X_train, y_train), (X_test, y_test) = mnist.load_data()
# print(X_train.shape)
# pyplot.imshow(X_train[0])
# pyplot.show()

# добавляем к двумерному изображению глубину равную 1, т.е. из (n, ширина, высота) получаем (n, глубина, ширина, высота)
print(X_train[1])
X_train = X_train.reshape(X_train.shape[0], 512, 512, 1)
X_test = X_test.reshape(X_test.shape[0], 512, 512, 1)
print(X_train.shape)
# print(X_train[1])

# нормализация данных
X_train = X_train.astype('float32')
X_test = X_test.astype('float32')
X_train /= 255.0
X_test /= 255.0

# print(Y_train.shape)
# print(y_train[1])
# Y_train = np_utils.to_categorical(Y_train, 10)
# Y_test = np_utils.to_categorical(y_test, 10)
# print(Y_train.shape)
# print(y_train[1])
# file = open("out.txt", "w")
# file.write(X_train.ToString)
# file.close

# создание модели
model = Sequential()
model.add(Conv2D(filters=32,kernel_size=(3, 3), activation = 'relu', input_shape=(512, 512, 1)))

# print(model.output_shape)
model.add(Conv2D(32, (3, 3), activation='relu'))
model.add(MaxPooling2D(pool_size=(2,2)))
model.add(Dropout(0.25))

model.add(Flatten())

model.add(Dense(128, activation='relu'))
model.add(Dropout(0.5))
model.add(Dense(48))

# компиляция модели с оптимизатором Adam
model.compile(loss='mse', optimizer='Adam', metrics=['MeanSquaredError'])

# обучение модели на тренировочных данных
model.fit(X_train, Y_train, batch_size=32, epochs=3, verbose=1)

# оценка модели на тестовых данных
results = model.evaluate(X_test, Y_test, batch_size=128)
print('test loss, test acc:', results)
print('\n# Генерируем прогнозы для 10 образцов')
predictions = model.predict(X_test[:10])
print('прогноз:', predictions[:10])
print('ответ:', Y_test[:10])