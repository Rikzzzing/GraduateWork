import numpy as np
from stl import mesh
# import matplotlib.pyplot as plt
import matplotlib.pyplot as plt
# from mpl_toolkits.mplot3d import Axes3D
from scipy.spatial import ConvexHull
from scipy.spatial import Delaunay

points_src = []
points = []

# file = open(r'D:\Python\vertex_model\Vertex_Info_1.txt')
file = open(r'E:\Unity\DiplomRaycast\Python\Vertex_Info_1.txt')

for line in file:
        points_src.append([float(x) for x in line.split(', ')])


# Убрать лишние элементы
for pnt in points_src:
    if pnt not in points:
        points.append(pnt)

print("===points===\n")
for pnt in points:
    print(str(pnt))


# vertexes = np.array(points_src)
# print("vertexes: \n" + str(vertexes))

# # points = [
# #     [-3, -3, 0],
# #     [+3, -3, 0],
# #     [+3, +3, 0],
# #     [-3, +3, 0],
# #     [+0, +0, +3]
# # ]

vertexes = np.array(points)

vertexes_2d = np.zeros((vertexes.shape[0],2))

# print("vertexes: " + str(vertexes))
# print("vertexes_2d: " + str(vertexes_2d))
# print("vertexes size: " + str(vertexes.shape[0]))

for i in range (vertexes.shape[0]):
    vertex = np.array(vertexes[i,:])
    vertexes_2d[i][0] = vertex[0]
    vertexes_2d[i][1] = vertex[2]
    # print("vertex: " + str(vertex[0]) + " " + str(vertex[1]))

print("vertexes_2d: \n" + str(vertexes_2d))

# -----------------------------------------
# Delaunay debug

tri = Delaunay(vertexes_2d)

plt.triplot(vertexes_2d[:,0], vertexes_2d[:,1], tri.simplices.copy())
plt.plot(vertexes_2d[:,0], vertexes_2d[:,1], 'o')
plt.show()
# -----------------------------------------


# hull = ConvexHull(vertexes, incremental = True, qhull_options="Qa")0
# faces = hull.simplices
faces = tri.simplices

# print("faces" + str(faces))

# Create the mesh
cube = mesh.Mesh(np.zeros(faces.shape[0], dtype=mesh.Mesh.dtype))
for i, f in enumerate(faces):
    for j in range(3):
        cube.vectors[i][j] = vertexes[f[j],:]

# print(cube.vectors)
# Write the mesh to file "cube.stl"
cube.save(r'E:\Unity\DiplomRaycast\Python\object_test.stl')
