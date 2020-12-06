// MarkDraw.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"

#include <gl/GL.h>
#include <gl/GLU.h>

extern "C" __declspec(dllexport) void __stdcall DrawMark(HDC hDC)
{
	//const PIXELFORMATDESCRIPTOR pixelFormatDesc =
	//{
	//	0, 1, PFD_SUPPORT_OPENGL | PFD_DOUBLEBUFFER, 32, 0, 0, 0,
	//	0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 32, 0, 0, 0, 0, 0, 0, 0
	//};

	PIXELFORMATDESCRIPTOR pixelFormatDesc =
	{
		sizeof(PIXELFORMATDESCRIPTOR),
		1,
		PFD_DRAW_TO_WINDOW | PFD_SUPPORT_OPENGL | PFD_DOUBLEBUFFER, //Flags
		PFD_TYPE_RGBA,                                              //The kind of framebuffer. RGBA or palette.
		32,                                                         //Colordepth of the framebuffer.
		0, 0, 0, 0, 0, 0,
		0,
		0,
		0,
		0, 0, 0, 0,
		24,                                                         //Number of bits for the depthbuffer
		8,                                                          //Number of bits for the stencilbuffer
		0,                                                          //Number of Aux buffers in the framebuffer.
		PFD_MAIN_PLANE,
		0,
		0, 0, 0
	};

	int format = ChoosePixelFormat(hDC, &pixelFormatDesc);
	if (format == 0)
	{
		return;
	}

	if( !::SetPixelFormat(hDC, format, &pixelFormatDesc) )
	{
		auto result = ::GetLastError();
		return;
	}

	HGLRC glRC = ::wglCreateContext(hDC);
	if ( !::wglMakeCurrent(hDC, glRC) )
	{
//		auto result = ::GetLastError();
//		return;
	}

	::glClearColor(0.0f, 0.5f, 1.0f, 1.0f);
	::glClear(GL_COLOR_BUFFER_BIT);

	::glRectf(-0.5f, -0.5f, 0.5f, 0.5f);

	::glFlush();
	::SwapBuffers(hDC);

	::wglMakeCurrent(NULL, NULL);
	::wglDeleteContext(glRC);
}

extern "C" __declspec(dllexport) void __stdcall DrawMark2(HBITMAP hBitmap)
{
	BITMAPINFO bmpInfo;
	LPDWORD lpPixel;
	//HBITMAP hBitmap;
	HDC hMemDC;

	////DIBの情報を設定する
	//bmpInfo.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
	//bmpInfo.bmiHeader.biWidth = 500;
	//bmpInfo.bmiHeader.biHeight = 500;
	//bmpInfo.bmiHeader.biPlanes = 1;
	//bmpInfo.bmiHeader.biBitCount = 32;
	//bmpInfo.bmiHeader.biCompression = BI_RGB;

	////DIBSection作成
	auto hScreenDC = GetDC(NULL);
	hBitmap = CreateDIBSection(hScreenDC, &bmpInfo, DIB_RGB_COLORS, (void**)&lpPixel, NULL, 0);
	hMemDC = CreateCompatibleDC(hScreenDC);
	SelectObject(hMemDC, hBitmap);
	ReleaseDC(NULL, hScreenDC);



	//const PIXELFORMATDESCRIPTOR pixelFormatDesc =
	//{
	//	0, 1, PFD_SUPPORT_OPENGL | PFD_DOUBLEBUFFER, 32, 0, 0, 0,
	//	0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 32, 0, 0, 0, 0, 0, 0, 0
	//};

	PIXELFORMATDESCRIPTOR pixelFormatDesc =
	{
		sizeof(PIXELFORMATDESCRIPTOR),
		1,
		PFD_DRAW_TO_WINDOW | PFD_SUPPORT_OPENGL | PFD_DOUBLEBUFFER, //Flags
		PFD_TYPE_RGBA,                                              //The kind of framebuffer. RGBA or palette.
		32,                                                         //Colordepth of the framebuffer.
		0, 0, 0, 0, 0, 0,
		0,
		0,
		0,
		0, 0, 0, 0,
		24,                                                         //Number of bits for the depthbuffer
		8,                                                          //Number of bits for the stencilbuffer
		0,                                                          //Number of Aux buffers in the framebuffer.
		PFD_MAIN_PLANE,
		0,
		0, 0, 0
	};

	int format = ChoosePixelFormat(hMemDC, &pixelFormatDesc);
	if (format == 0)
	{
		return;
	}

	if (!::SetPixelFormat(hMemDC, format, &pixelFormatDesc))
	{
		auto result = ::GetLastError();
		return;
	}

	HGLRC glRC = ::wglCreateContext(hMemDC);
	if (!::wglMakeCurrent(hMemDC, glRC))
	{
		auto result = ::GetLastError();
		return;
	}

	::glClearColor(0.0f, 0.5f, 1.0f, 1.0f);
	::glClear(GL_COLOR_BUFFER_BIT);

	::glRectf(-0.5f, -0.5f, 0.5f, 0.5f);

	::glFlush();
	::SwapBuffers(hMemDC);

	::wglMakeCurrent(NULL, NULL);
	::wglDeleteContext(glRC);
}

extern "C" __declspec(dllexport) void __stdcall DrawMark3(HDC hDC)
{
	PIXELFORMATDESCRIPTOR pixelFormatDesc =
	{
		sizeof(PIXELFORMATDESCRIPTOR),
		1,
		PFD_DRAW_TO_BITMAP | PFD_SUPPORT_OPENGL | PFD_SUPPORT_GDI,	//Flags
		PFD_TYPE_RGBA,                                              //The kind of framebuffer. RGBA or palette.
		32,                                                         //Colordepth of the framebuffer.
		0, 0, 0, 0, 0, 0,
		0,
		0,
		0,
		0, 0, 0, 0,
		24,                                                         //Number of bits for the depthbuffer
		8,                                                          //Number of bits for the stencilbuffer
		0,                                                          //Number of Aux buffers in the framebuffer.
		PFD_MAIN_PLANE,
		0,
		0, 0, 0
	};

	int format = ChoosePixelFormat(hDC, &pixelFormatDesc);
	if (format == 0)
	{
		return;
	}

	if (!::SetPixelFormat(hDC, format, &pixelFormatDesc))
	{
		auto result = ::GetLastError();
		return;
	}

	HGLRC glRC = ::wglCreateContext(hDC);
	if (!::wglMakeCurrent(hDC, glRC))
	{
		auto result = ::GetLastError();
		return;
	}

	::glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
//	::glClear(GL_COLOR_BUFFER_BIT);

	::glEnable(GL_BLEND);
	::glBlendFunc(GL_ONE, GL_ONE);
	::glColor4f(0.5f, 0.5f, 0.5f, 0.0f);
	::glRectf(-0.5f, -0.5f, 0.5f, 0.5f);

	::glDisable(GL_BLEND);

	::glFlush();

	::wglMakeCurrent(NULL, NULL);
	::wglDeleteContext(glRC);
}
