import numpy
import keras

from keras import Sequential
from keras.utils import np_utils
from keras.layers import Conv2D, Dense, Dropout, Activation, Flatten
from keras.layers import Convolution2D, MaxPooling2D

# проверка среды и версий
print('numpy: ' + numpy.__version__)
print('keras: ' + keras.__version__)

# загрузка набора данных для обучения
arrays = numpy.load("dataset.npz")
# arrays = numpy.load("dataset (1 image, 1 projection).npz")
# arrays = numpy.load("dataset (3 image, 6 projection).npz")

X_train = arrays.f.arr_0
Y_train = arrays.f.arr_1
X_test = arrays.f.arr_2
Y_test = arrays.f.arr_3

# добавляем к двумерному изображению глубину равную 1, т.е. из (n, ширина, высота) получаем (n, ширина, высота, глубина)
X_train = X_train.reshape(X_train.shape[0], X_train.shape[1], X_train.shape[2], 1)
X_test = X_test.reshape(X_test.shape[0], X_test.shape[1], X_test.shape[2], 1)

# нормализация данных
X_train = X_train.astype('float32')
X_test = X_test.astype('float32')
X_train /= 255.0
X_test /= 255.0

# создание модели
model = Sequential()
model.add(Conv2D(filters=32,kernel_size=(3, 3), activation = 'relu', input_shape=(X_test.shape[1], X_test.shape[2], 1)))

# print(model.output_shape)
model.add(Conv2D(32, (3, 3), activation='relu'))
model.add(MaxPooling2D(pool_size=(2,2)))
model.add(Dropout(0.25))

model.add(Flatten())

model.add(Dense(128, activation='relu'))
model.add(Dropout(0.25))
model.add(Dense(128))
model.add(Dropout(0.25))
model.add(Dense(64))
model.add(Dropout(0.1))
model.add(Dense(48))

# компиляция модели с оптимизатором Adam
model.compile(loss='mse', optimizer='Adam', metrics=['MeanSquaredError'])

# обучение модели на тренировочных данных
model.fit(X_train, Y_train, batch_size=32, epochs=10, verbose=1)

# сохранение модли
model.save("model")

# оценка модели на тестовых данных
results = model.evaluate(X_test, Y_test, batch_size=128)
print('test loss, test acc:', results)
print('\n# Генерируем прогнозы для 10 образцов')
predictions = model.predict(X_test[:10])
print('прогноз:', predictions[:10])
print('ответ:', Y_test[:10])