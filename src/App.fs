module Brewlette

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Browser
open System

type Rectangle = {
    fill: U3<string,CanvasGradient,CanvasPattern>
    width:float
    height:float
}

type Shape =
    | Rectangle of Rectangle

type Node = {
    translate: float * float
    rotate: float
    children: Node list
    thingToDraw: Shape
}

let bg = Rectangle {
    fill = U3.Case1 "black"
    width = 1000.
    height = 800.
}

let rect1 = Rectangle {
    fill = !^"rgb(200,0,0)"
    width = 55.
    height = 50.
}
let rect2 = Rectangle {
    fill = !^"rgba(0, 0, 200, 0.5)"
    width = 55.
    height = 50.
}

let scene =
    {
        translate = 500., 400.
        rotate = 0.
        thingToDraw = bg
        children =
            [{
                translate = 0., 0.
                rotate = (Math.PI/4.)
                thingToDraw = rect1
                children =
                    [{
                        translate = 20., 20.
                        rotate = -(Math.PI/4.)
                        thingToDraw = rect2
                        children = []
                    }]
            }]
    }

let drawRect (ctx:CanvasRenderingContext2D) rect =
    let x1, y1 = 0. - rect.width / 2., 0. - rect.height / 2.
    ctx.fillStyle <- rect.fill
    ctx.fillRect (x1, y1, rect.width, rect.height)

let drawShape (ctx:CanvasRenderingContext2D) shape =
    match shape with
    | Rectangle rect -> drawRect ctx rect

let rec drawNode (ctx:CanvasRenderingContext2D) (x,y) n =
    let (tx, ty) = n.translate
    let (x1, y1) = (x + tx, y + ty)

    ctx.save()
    ctx.translate (tx,ty)
    ctx.rotate (n.rotate)
    drawShape ctx n.thingToDraw
    n.children
    |> Seq.iter (drawNode ctx (x1, y1))
    ctx.restore()

let init() =
    let canvas = Browser.document.getElementsByTagName_canvas().[0]
    canvas.width <- 1000.
    canvas.height <- 800.
    let ctx = canvas.getContext_2d()

    drawNode ctx (500., 400.) scene

init()