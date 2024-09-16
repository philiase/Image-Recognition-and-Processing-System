#include <opencv2/opencv.hpp>
#include <iostream>

using namespace cv;
using namespace std;

// Function for edge detection using Canny
Mat edgeDetection(Mat img) {
    Mat edges;
    Canny(img, edges, 100, 200);
    return edges;
}

// Function for object detection using Haar Cascades
void detectObjects(Mat img) {
    CascadeClassifier face_cascade;
    face_cascade.load("haarcascade_frontalface_default.xml");

    vector<Rect> faces;
    face_cascade.detectMultiScale(img, faces, 1.1, 4, 0, Size(30, 30));

    for (size_t i = 0; i < faces.size(); i++) {
        rectangle(img, faces[i], Scalar(255, 0, 0), 2);
    }
}

// Function for image enhancement (Gaussian blur)
Mat enhanceImage(Mat img) {
    Mat enhanced;
    GaussianBlur(img, enhanced, Size(15, 15), 0);
    return enhanced;
}

extern "C" {
    // Wrapper for C# to call edge detection
    __declspec(dllexport) void ProcessEdges(const char* inputPath, const char* outputPath) {
        Mat img = imread(inputPath, IMREAD_GRAYSCALE);
        Mat edges = edgeDetection(img);
        imwrite(outputPath, edges);
    }

    // Wrapper for C# to call object detection
    __declspec(dllexport) void ProcessObjectDetection(const char* inputPath, const char* outputPath) {
        Mat img = imread(inputPath);
        detectObjects(img);
        imwrite(outputPath, img);
    }

    // Wrapper for C# to call image enhancement
    __declspec(dllexport) void ProcessEnhancement(const char* inputPath, const char* outputPath) {
        Mat img = imread(inputPath);
        Mat enhanced = enhanceImage(img);
        imwrite(outputPath, enhanced);
    }
}
