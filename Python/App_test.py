import numpy as np
from stl import mesh
# import matplotlib.pyplot as plt
# from mpl_toolkits.mplot3d import Axes3D
from scipy.spatial import ConvexHull

points = []

file = open(r'E:\Unity\DiplomRaycast\Python\Vertex_Info_1.txt')

for line in file:
        points.append([float(x) for x in line.split(', ')])

vertexes = np.array(points)

hull = ConvexHull(vertexes, incremental = True, qhull_options="Qx")
faces = hull.simplices

# print("faces" + str(faces))

# Create the mesh
cube = mesh.Mesh(np.zeros(faces.shape[0], dtype=mesh.Mesh.dtype))
for i, f in enumerate(faces):
    for j in range(3):
        cube.vectors[i][j] = vertexes[f[j],:]

# #print(cube.vectors)
# # Write the mesh to file "cube.stl"
cube.save(r'E:\Unity\DiplomRaycast\Python\cube_test.stl')
