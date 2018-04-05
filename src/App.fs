module Brewlette

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Browser

type Rectangle = {
    fill: U3<string,CanvasGradient,CanvasPattern>
    width:float
    height:float
}

let drawBg (canvas:HTMLCanvasElement) (ctx:CanvasRenderingContext2D) f =
    ctx.fillStyle <- f
    ctx.fillRect (0., 0., canvas.width, canvas.height)

let drawRect (canvas:HTMLCanvasElement) (ctx:CanvasRenderingContext2D) rect x y =
    ctx.fillStyle <- rect.fill
    ctx.fillRect (x, y, rect.width, rect.height)

let init() =
    let canvas = Browser.document.getElementsByTagName_canvas().[0]
    canvas.width <- 1000.
    canvas.height <- 800.
    let ctx = canvas.getContext_2d()

    drawBg canvas ctx <| U3.Case1 "black"
    let rect1 = {
        fill = !^"rgb(200,0,0)"
        width = 55.
        height = 50.
    }
    let rect2 = {
        fill = !^"rgba(0, 0, 200, 0.5)"
        width = 55.
        height = 50.
    }
    drawRect canvas ctx rect1 10. 10.
    drawRect canvas ctx rect2 30. 30.

init()