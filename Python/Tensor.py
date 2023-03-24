import tensorflow as tf
from keras.datasets import mnist
from keras.utils import to_categorical

# загрузка данных для обучения
(x_train, y_train), (x_test, y_test) = mnist.load_data()

#нормализация данных
x_train = x_train / 255
x_test = x_test / 255

x_train = tf.reshape(tf.cast(x_train, tf.float32), [-1, 28*28])
x_test = tf.reshape(tf.cast(x_test, tf.float32), [-1, 28*28])

y_train = to_categorical(y_train, 10)

# модель полносвязного слоя
class FullConnectLayer(tf.Module):
    def __init__(self, outputs, activate = "relu"):
        super().__init__()
        self.outputs = outputs
        self.activate = activate
        self.fl_init = False
    
    def __call__(self, x):
        if not self.fl_init:
            self.w = tf.random.truncated_normal((x.shape[-1], self.outputs), stddev = 0.1, name = "w")
            self.b = tf.zeros([self.outputs], dtype = tf.float32, name = "b")

            self.w = tf.Variable(self.w)
            self.b = tf.Variable(self.b)

            self.fl_init = True
        
        y = x @ self.w + self.b

        if self.activate == "relu":
            return tf.nn.relu(y)
        elif self.activate == "softmax":
            return tf.nn.softmax(y)
        
        return y

layer_1 = FullConnectLayer(128)
layer_2 = FullConnectLayer(10, activate = "softmax")