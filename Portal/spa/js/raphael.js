﻿!function t(e, r) {
    "object" == typeof exports && "object" == typeof module ? module.exports = r() : "function" == typeof define && define.amd ? define([], r) : "object" == typeof exports ? exports.Raphael = r() : e.Raphael = r()
} (this, function () {
    return function (t) {
        function e(i) {
            if (r[i]) return r[i].exports;
            var n = r[i] = {
                exports: {},
                id: i,
                loaded: !1
            };
            return t[i].call(n.exports, n, n.exports, e), n.loaded = !0, n.exports
        }
        var r = {};
        return e.m = t, e.c = r, e.p = "", e(0)
    } ([
        function (t, e, r) {
            var i, n;
            i = [r(1), r(3), r(4)], n = function (t) {
                return t
            } .apply(e, i), !(void 0 !== n && (t.exports = n))
        },
        function (t, e, r) {
            var i, n;
            i = [r(2)], n = function (t) {
                function e(r) {
                    if (e.is(r, "function")) return w ? r() : t.on("raphael.DOMload", r);
                    if (e.is(r, Q)) return e._engine.create[z](e, r.splice(0, 3 + e.is(r[0], $))).add(r);
                    var i = Array.prototype.slice.call(arguments, 0);
                    if (e.is(i[i.length - 1], "function")) {
                        var n = i.pop();
                        return w ? n.call(e._engine.create[z](e, i)) : t.on("raphael.DOMload", function () {
                            n.call(e._engine.create[z](e, i))
                        })
                    }
                    return e._engine.create[z](e, arguments)
                }

                function r(t) {
                    if ("function" == typeof t || Object(t) !== t) return t;
                    var e = new t.constructor;
                    for (var i in t) t[T](i) && (e[i] = r(t[i]));
                    return e
                }

                function i(t, e) {
                    for (var r = 0, i = t.length; i > r; r++)
                        if (t[r] === e) return t.push(t.splice(r, 1)[0])
                }

                function n(t, e, r) {
                    function n() {
                        var a = Array.prototype.slice.call(arguments, 0),
                            s = a.join("␀"),
                            o = n.cache = n.cache || {}, l = n.count = n.count || [];
                        return o[T](s) ? (i(l, s), r ? r(o[s]) : o[s]) : (l.length >= 1e3 && delete o[l.shift()], l.push(s), o[s] = t[z](e, a), r ? r(o[s]) : o[s])
                    }
                    return n
                }

                function a() {
                    return this.hex
                }

                function s(t, e) {
                    for (var r = [], i = 0, n = t.length; n - 2 * !e > i; i += 2) {
                        var a = [{
                            x: +t[i - 2],
                            y: +t[i - 1]
                        }, {
                            x: +t[i],
                            y: +t[i + 1]
                        }, {
                            x: +t[i + 2],
                            y: +t[i + 3]
                        }, {
                            x: +t[i + 4],
                            y: +t[i + 5]
                        }];
                        e ? i ? n - 4 == i ? a[3] = {
                            x: +t[0],
                            y: +t[1]
                        } : n - 2 == i && (a[2] = {
                            x: +t[0],
                            y: +t[1]
                        }, a[3] = {
                            x: +t[2],
                            y: +t[3]
                        }) : a[0] = {
                            x: +t[n - 2],
                            y: +t[n - 1]
                        } : n - 4 == i ? a[3] = a[2] : i || (a[0] = {
                            x: +t[i],
                            y: +t[i + 1]
                        }), r.push(["C", (-a[0].x + 6 * a[1].x + a[2].x) / 6, (-a[0].y + 6 * a[1].y + a[2].y) / 6, (a[1].x + 6 * a[2].x - a[3].x) / 6, (a[1].y + 6 * a[2].y - a[3].y) / 6, a[2].x, a[2].y])
                    }
                    return r
                }

                function o(t, e, r, i, n) {
                    var a = -3 * e + 9 * r - 9 * i + 3 * n,
                        s = t * a + 6 * e - 12 * r + 6 * i;
                    return t * s - 3 * e + 3 * r
                }

                function l(t, e, r, i, n, a, s, l, h) {
                    null == h && (h = 1), h = h > 1 ? 1 : 0 > h ? 0 : h;
                    for (var u = h / 2, c = 12, f = [-.1252, .1252, -.3678, .3678, -.5873, .5873, -.7699, .7699, -.9041, .9041, -.9816, .9816], p = [.2491, .2491, .2335, .2335, .2032, .2032, .1601, .1601, .1069, .1069, .0472, .0472], d = 0, g = 0; c > g; g++) {
                        var x = u * f[g] + u,
                            v = o(x, t, r, n, s),
                            y = o(x, e, i, a, l),
                            m = v * v + y * y;
                        d += p[g] * Y.sqrt(m)
                    }
                    return u * d
                }

                function h(t, e, r, i, n, a, s, o, h) {
                    if (!(0 > h || l(t, e, r, i, n, a, s, o) < h)) {
                        var u = 1,
                            c = u / 2,
                            f = u - c,
                            p, d = .01;
                        for (p = l(t, e, r, i, n, a, s, o, f); H(p - h) > d; ) c /= 2, f += (h > p ? 1 : -1) * c, p = l(t, e, r, i, n, a, s, o, f);
                        return f
                    }
                }

                function u(t, e, r, i, n, a, s, o) {
                    if (!(W(t, r) < G(n, s) || G(t, r) > W(n, s) || W(e, i) < G(a, o) || G(e, i) > W(a, o))) {
                        var l = (t * i - e * r) * (n - s) - (t - r) * (n * o - a * s),
                            h = (t * i - e * r) * (a - o) - (e - i) * (n * o - a * s),
                            u = (t - r) * (a - o) - (e - i) * (n - s);
                        if (u) {
                            var c = l / u,
                                f = h / u,
                                p = +c.toFixed(2),
                                d = +f.toFixed(2);
                            if (!(p < +G(t, r).toFixed(2) || p > +W(t, r).toFixed(2) || p < +G(n, s).toFixed(2) || p > +W(n, s).toFixed(2) || d < +G(e, i).toFixed(2) || d > +W(e, i).toFixed(2) || d < +G(a, o).toFixed(2) || d > +W(a, o).toFixed(2))) return {
                                x: c,
                                y: f
                            }
                        }
                    }
                }

                function c(t, e) {
                    return p(t, e)
                }

                function f(t, e) {
                    return p(t, e, 1)
                }

                function p(t, r, i) {
                    var n = e.bezierBBox(t),
                        a = e.bezierBBox(r);
                    if (!e.isBBoxIntersect(n, a)) return i ? 0 : [];
                    for (var s = l.apply(0, t), o = l.apply(0, r), h = W(~ ~(s / 5), 1), c = W(~ ~(o / 5), 1), f = [], p = [], d = {}, g = i ? 0 : [], x = 0; h + 1 > x; x++) {
                        var v = e.findDotsAtSegment.apply(e, t.concat(x / h));
                        f.push({
                            x: v.x,
                            y: v.y,
                            t: x / h
                        })
                    }
                    for (x = 0; c + 1 > x; x++) v = e.findDotsAtSegment.apply(e, r.concat(x / c)), p.push({
                        x: v.x,
                        y: v.y,
                        t: x / c
                    });
                    for (x = 0; h > x; x++)
                        for (var y = 0; c > y; y++) {
                            var m = f[x],
                                b = f[x + 1],
                                _ = p[y],
                                w = p[y + 1],
                                k = H(b.x - m.x) < .001 ? "y" : "x",
                                B = H(w.x - _.x) < .001 ? "y" : "x",
                                C = u(m.x, m.y, b.x, b.y, _.x, _.y, w.x, w.y);
                            if (C) {
                                if (d[C.x.toFixed(4)] == C.y.toFixed(4)) continue;
                                d[C.x.toFixed(4)] = C.y.toFixed(4);
                                var S = m.t + H((C[k] - m[k]) / (b[k] - m[k])) * (b.t - m.t),
                                    T = _.t + H((C[B] - _[B]) / (w[B] - _[B])) * (w.t - _.t);
                                S >= 0 && 1.001 >= S && T >= 0 && 1.001 >= T && (i ? g++ : g.push({
                                    x: C.x,
                                    y: C.y,
                                    t1: G(S, 1),
                                    t2: G(T, 1)
                                }))
                            }
                        }
                    return g
                }

                function d(t, r, i) {
                    t = e._path2curve(t), r = e._path2curve(r);
                    for (var n, a, s, o, l, h, u, c, f, d, g = i ? 0 : [], x = 0, v = t.length; v > x; x++) {
                        var y = t[x];
                        if ("M" == y[0]) n = l = y[1], a = h = y[2];
                        else {
                            "C" == y[0] ? (f = [n, a].concat(y.slice(1)), n = f[6], a = f[7]) : (f = [n, a, n, a, l, h, l, h], n = l, a = h);
                            for (var m = 0, b = r.length; b > m; m++) {
                                var _ = r[m];
                                if ("M" == _[0]) s = u = _[1], o = c = _[2];
                                else {
                                    "C" == _[0] ? (d = [s, o].concat(_.slice(1)), s = d[6], o = d[7]) : (d = [s, o, s, o, u, c, u, c], s = u, o = c);
                                    var w = p(f, d, i);
                                    if (i) g += w;
                                    else {
                                        for (var k = 0, B = w.length; B > k; k++) w[k].segment1 = x, w[k].segment2 = m, w[k].bez1 = f, w[k].bez2 = d;
                                        g = g.concat(w)
                                    }
                                }
                            }
                        }
                    }
                    return g
                }

                function g(t, e, r, i, n, a) {
                    null != t ? (this.a = +t, this.b = +e, this.c = +r, this.d = +i, this.e = +n, this.f = +a) : (this.a = 1, this.b = 0, this.c = 0, this.d = 1, this.e = 0, this.f = 0)
                }

                function x() {
                    return this.x + I + this.y
                }

                function v() {
                    return this.x + I + this.y + I + this.width + " × " + this.height
                }

                function y(t, e, r, i, n, a) {
                    function s(t) {
                        return ((c * t + u) * t + h) * t
                    }

                    function o(t, e) {
                        var r = l(t, e);
                        return ((d * r + p) * r + f) * r
                    }

                    function l(t, e) {
                        var r, i, n, a, o, l;
                        for (n = t, l = 0; 8 > l; l++) {
                            if (a = s(n) - t, H(a) < e) return n;
                            if (o = (3 * c * n + 2 * u) * n + h, H(o) < 1e-6) break;
                            n -= a / o
                        }
                        if (r = 0, i = 1, n = t, r > n) return r;
                        if (n > i) return i;
                        for (; i > r; ) {
                            if (a = s(n), H(a - t) < e) return n;
                            t > a ? r = n : i = n, n = (i - r) / 2 + r
                        }
                        return n
                    }
                    var h = 3 * e,
                        u = 3 * (i - e) - h,
                        c = 1 - h - u,
                        f = 3 * r,
                        p = 3 * (n - r) - f,
                        d = 1 - f - p;
                    return o(t, 1 / (200 * a))
                }

                function m(t, e) {
                    var r = [],
                        i = {};
                    if (this.ms = e, this.times = 1, t) {
                        for (var n in t) t[T](n) && (i[ht(n)] = t[n], r.push(ht(n)));
                        r.sort(Bt)
                    }
                    this.anim = i, this.top = r[r.length - 1], this.percents = r
                }

                function b(r, i, n, a, s, o) {
                    n = ht(n);
                    var l, h, u, c = [],
                        f, p, d, x = r.ms,
                        v = {}, m = {}, b = {};
                    if (a)
                        for (w = 0, B = Ee.length; B > w; w++) {
                            var _ = Ee[w];
                            if (_.el.id == i.id && _.anim == r) {
                                _.percent != n ? (Ee.splice(w, 1), u = 1) : h = _, i.attr(_.totalOrigin);
                                break
                            }
                        } else a = +m;
                    for (var w = 0, B = r.percents.length; B > w; w++) {
                        if (r.percents[w] == n || r.percents[w] > a * r.top) {
                            n = r.percents[w], p = r.percents[w - 1] || 0, x = x / r.top * (n - p), f = r.percents[w + 1], l = r.anim[n];
                            break
                        }
                        a && i.attr(r.anim[r.percents[w]])
                    }
                    if (l) {
                        if (h) h.initstatus = a, h.start = new Date - h.ms * a;
                        else {
                            for (var C in l)
                                if (l[T](C) && (pt[T](C) || i.paper.customAttributes[T](C))) switch (v[C] = i.attr(C), null == v[C] && (v[C] = ft[C]), m[C] = l[C], pt[C]) {
                                    case $:
                                        b[C] = (m[C] - v[C]) / x;
                                        break;
                                    case "colour":
                                        v[C] = e.getRGB(v[C]);
                                        var S = e.getRGB(m[C]);
                                        b[C] = {
                                            r: (S.r - v[C].r) / x,
                                            g: (S.g - v[C].g) / x,
                                            b: (S.b - v[C].b) / x
                                        };
                                        break;
                                    case "path":
                                        var A = Qt(v[C], m[C]),
                                            E = A[1];
                                        for (v[C] = A[0], b[C] = [], w = 0, B = v[C].length; B > w; w++) {
                                            b[C][w] = [0];
                                            for (var N = 1, M = v[C][w].length; M > N; N++) b[C][w][N] = (E[w][N] - v[C][w][N]) / x
                                        }
                                        break;
                                    case "transform":
                                        var L = i._,
                                            z = le(L[C], m[C]);
                                        if (z)
                                            for (v[C] = z.from, m[C] = z.to, b[C] = [], b[C].real = !0, w = 0, B = v[C].length; B > w; w++)
                                                for (b[C][w] = [v[C][w][0]], N = 1, M = v[C][w].length; M > N; N++) b[C][w][N] = (m[C][w][N] - v[C][w][N]) / x;
                                        else {
                                            var F = i.matrix || new g,
                                                R = {
                                                    _: {
                                                        transform: L.transform
                                                    },
                                                    getBBox: function () {
                                                        return i.getBBox(1)
                                                    }
                                                };
                                            v[C] = [F.a, F.b, F.c, F.d, F.e, F.f], se(R, m[C]), m[C] = R._.transform, b[C] = [(R.matrix.a - F.a) / x, (R.matrix.b - F.b) / x, (R.matrix.c - F.c) / x, (R.matrix.d - F.d) / x, (R.matrix.e - F.e) / x, (R.matrix.f - F.f) / x]
                                        }
                                        break;
                                    case "csv":
                                        var I = j(l[C])[q](k),
                                            D = j(v[C])[q](k);
                                        if ("clip-rect" == C)
                                            for (v[C] = D, b[C] = [], w = D.length; w--; ) b[C][w] = (I[w] - v[C][w]) / x;
                                        m[C] = I;
                                        break;
                                    default:
                                        for (I = [][P](l[C]), D = [][P](v[C]), b[C] = [], w = i.paper.customAttributes[C].length; w--; ) b[C][w] = ((I[w] || 0) - (D[w] || 0)) / x
                                }
                            var V = l.easing, O = e.easing_formulas[V];
                            if (!O)
                                if (O = j(V).match(st), O && 5 == O.length) {
                                    var Y = O;
                                    O = function (t) {
                                        return y(t, +Y[1], +Y[2], +Y[3], +Y[4], x)
                                    }
                                } else O = St;
                            if (d = l.start || r.start || +new Date, _ = {
                                anim: r,
                                percent: n,
                                timestamp: d,
                                start: d + (r.del || 0),
                                status: 0,
                                initstatus: a || 0,
                                stop: !1,
                                ms: x,
                                easing: O,
                                from: v,
                                diff: b,
                                to: m,
                                el: i,
                                callback: l.callback,
                                prev: p,
                                next: f,
                                repeat: o || r.times,
                                origin: i.attr(),
                                totalOrigin: s
                            }, Ee.push(_), a && !h && !u && (_.stop = !0, _.start = new Date - x * a, 1 == Ee.length)) return Me();
                            u && (_.start = new Date - _.ms * a), 1 == Ee.length && Ne(Me)
                        }
                        t("raphael.anim.start." + i.id, i, r)
                    }
                }

                function _(t) {
                    for (var e = 0; e < Ee.length; e++) Ee[e].el.paper == t && Ee.splice(e--, 1)
                }
                e.version = "2.2.0", e.eve = t;
                var w, k = /[, ]+/,
                    B = {
                        circle: 1,
                        rect: 1,
                        path: 1,
                        ellipse: 1,
                        text: 1,
                        image: 1
                    }, C = /\{(\d+)\}/g,
                    S = "prototype",
                    T = "hasOwnProperty",
                    A = {
                        doc: document,
                        win: window
                    }, E = {
                        was: Object.prototype[T].call(A.win, "Raphael"),
                        is: A.win.Raphael
                    }, N = function () {
                        this.ca = this.customAttributes = {}
                    }, M, L = "appendChild",
                    z = "apply",
                    P = "concat",
                    F = "ontouchstart" in A.win || A.win.DocumentTouch && A.doc instanceof DocumentTouch,
                    R = "",
                    I = " ",
                    j = String,
                    q = "split",
                    D = "click dblclick mousedown mousemove mouseout mouseover mouseup touchstart touchmove touchend touchcancel"[q](I),
                    V = {
                        mousedown: "touchstart",
                        mousemove: "touchmove",
                        mouseup: "touchend"
                    }, O = j.prototype.toLowerCase,
                    Y = Math,
                    W = Y.max,
                    G = Y.min,
                    H = Y.abs,
                    X = Y.pow,
                    U = Y.PI,
                    $ = "number",
                    Z = "string",
                    Q = "array",
                    J = "toString",
                    K = "fill",
                    tt = Object.prototype.toString,
                    et = {}, rt = "push",
                    it = e._ISURL = /^url\(['"]?(.+?)['"]?\)$/i,
                    nt = /^\s*((#[a-f\d]{6})|(#[a-f\d]{3})|rgba?\(\s*([\d\.]+%?\s*,\s*[\d\.]+%?\s*,\s*[\d\.]+%?(?:\s*,\s*[\d\.]+%?)?)\s*\)|hsba?\(\s*([\d\.]+(?:deg|\xb0|%)?\s*,\s*[\d\.]+%?\s*,\s*[\d\.]+(?:%?\s*,\s*[\d\.]+)?)%?\s*\)|hsla?\(\s*([\d\.]+(?:deg|\xb0|%)?\s*,\s*[\d\.]+%?\s*,\s*[\d\.]+(?:%?\s*,\s*[\d\.]+)?)%?\s*\))\s*$/i,
                    at = {
                        NaN: 1,
                        Infinity: 1,
                        "-Infinity": 1
                    }, st = /^(?:cubic-)?bezier\(([^,]+),([^,]+),([^,]+),([^\)]+)\)/,
                    ot = Y.round,
                    lt = "setAttribute",
                    ht = parseFloat,
                    ut = parseInt,
                    ct = j.prototype.toUpperCase,
                    ft = e._availableAttrs = {
                        "arrow-end": "none",
                        "arrow-start": "none",
                        blur: 0,
                        "clip-rect": "0 0 1e9 1e9",
                        cursor: "default",
                        cx: 0,
                        cy: 0,
                        fill: "#fff",
                        "fill-opacity": 1,
                        font: '10px "Arial"',
                        "font-family": '"Arial"',
                        "font-size": "10",
                        "font-style": "normal",
                        "font-weight": 400,
                        gradient: 0,
                        height: 0,
                        href: "http://raphaeljs.com/",
                        "letter-spacing": 0,
                        opacity: 1,
                        path: "M0,0",
                        r: 0,
                        rx: 0,
                        ry: 0,
                        src: "",
                        stroke: "#000",
                        "stroke-dasharray": "",
                        "stroke-linecap": "butt",
                        "stroke-linejoin": "butt",
                        "stroke-miterlimit": 0,
                        "stroke-opacity": 1,
                        "stroke-width": 1,
                        target: "_blank",
                        "text-anchor": "middle",
                        title: "Raphael",
                        transform: "",
                        width: 0,
                        x: 0,
                        y: 0,
                        "class": ""
                    }, pt = e._availableAnimAttrs = {
                        blur: $,
                        "clip-rect": "csv",
                        cx: $,
                        cy: $,
                        fill: "colour",
                        "fill-opacity": $,
                        "font-size": $,
                        height: $,
                        opacity: $,
                        path: "path",
                        r: $,
                        rx: $,
                        ry: $,
                        stroke: "colour",
                        "stroke-opacity": $,
                        "stroke-width": $,
                        transform: "transform",
                        width: $,
                        x: $,
                        y: $
                    }, dt = /[\x09\x0a\x0b\x0c\x0d\x20\xa0\u1680\u180e\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u202f\u205f\u3000\u2028\u2029]/g,
                    gt = /[\x09\x0a\x0b\x0c\x0d\x20\xa0\u1680\u180e\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u202f\u205f\u3000\u2028\u2029]*,[\x09\x0a\x0b\x0c\x0d\x20\xa0\u1680\u180e\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u202f\u205f\u3000\u2028\u2029]*/,
                    xt = {
                        hs: 1,
                        rg: 1
                    }, vt = /,?([achlmqrstvxz]),?/gi,
                    yt = /([achlmrqstvz])[\x09\x0a\x0b\x0c\x0d\x20\xa0\u1680\u180e\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u202f\u205f\u3000\u2028\u2029,]*((-?\d*\.?\d*(?:e[\-+]?\d+)?[\x09\x0a\x0b\x0c\x0d\x20\xa0\u1680\u180e\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u202f\u205f\u3000\u2028\u2029]*,?[\x09\x0a\x0b\x0c\x0d\x20\xa0\u1680\u180e\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u202f\u205f\u3000\u2028\u2029]*)+)/gi,
                    mt = /([rstm])[\x09\x0a\x0b\x0c\x0d\x20\xa0\u1680\u180e\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u202f\u205f\u3000\u2028\u2029,]*((-?\d*\.?\d*(?:e[\-+]?\d+)?[\x09\x0a\x0b\x0c\x0d\x20\xa0\u1680\u180e\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u202f\u205f\u3000\u2028\u2029]*,?[\x09\x0a\x0b\x0c\x0d\x20\xa0\u1680\u180e\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u202f\u205f\u3000\u2028\u2029]*)+)/gi,
                    bt = /(-?\d*\.?\d*(?:e[\-+]?\d+)?)[\x09\x0a\x0b\x0c\x0d\x20\xa0\u1680\u180e\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u202f\u205f\u3000\u2028\u2029]*,?[\x09\x0a\x0b\x0c\x0d\x20\xa0\u1680\u180e\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u202f\u205f\u3000\u2028\u2029]*/gi,
                    _t = e._radial_gradient = /^r(?:\(([^,]+?)[\x09\x0a\x0b\x0c\x0d\x20\xa0\u1680\u180e\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u202f\u205f\u3000\u2028\u2029]*,[\x09\x0a\x0b\x0c\x0d\x20\xa0\u1680\u180e\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u202f\u205f\u3000\u2028\u2029]*([^\)]+?)\))?/,
                    wt = {}, kt = function (t, e) {
                        return t.key - e.key
                    }, Bt = function (t, e) {
                        return ht(t) - ht(e)
                    }, Ct = function () { }, St = function (t) {
                        return t
                    }, Tt = e._rectPath = function (t, e, r, i, n) {
                        return n ? [
                            ["M", t + n, e],
                            ["l", r - 2 * n, 0],
                            ["a", n, n, 0, 0, 1, n, n],
                            ["l", 0, i - 2 * n],
                            ["a", n, n, 0, 0, 1, -n, n],
                            ["l", 2 * n - r, 0],
                            ["a", n, n, 0, 0, 1, -n, -n],
                            ["l", 0, 2 * n - i],
                            ["a", n, n, 0, 0, 1, n, -n],
                            ["z"]
                        ] : [
                            ["M", t, e],
                            ["l", r, 0],
                            ["l", 0, i],
                            ["l", -r, 0],
                            ["z"]
                        ]
                    }, At = function (t, e, r, i) {
                        return null == i && (i = r), [
                            ["M", t, e],
                            ["m", 0, -i],
                            ["a", r, i, 0, 1, 1, 0, 2 * i],
                            ["a", r, i, 0, 1, 1, 0, -2 * i],
                            ["z"]
                        ]
                    }, Et = e._getPath = {
                        path: function (t) {
                            return t.attr("path")
                        },
                        circle: function (t) {
                            var e = t.attrs;
                            return At(e.cx, e.cy, e.r)
                        },
                        ellipse: function (t) {
                            var e = t.attrs;
                            return At(e.cx, e.cy, e.rx, e.ry)
                        },
                        rect: function (t) {
                            var e = t.attrs;
                            return Tt(e.x, e.y, e.width, e.height, e.r)
                        },
                        image: function (t) {
                            var e = t.attrs;
                            return Tt(e.x, e.y, e.width, e.height)
                        },
                        text: function (t) {
                            var e = t._getBBox();
                            return Tt(e.x, e.y, e.width, e.height)
                        },
                        set: function (t) {
                            var e = t._getBBox();
                            return Tt(e.x, e.y, e.width, e.height)
                        }
                    }, Nt = e.mapPath = function (t, e) {
                        if (!e) return t;
                        var r, i, n, a, s, o, l;
                        for (t = Qt(t), n = 0, s = t.length; s > n; n++)
                            for (l = t[n], a = 1, o = l.length; o > a; a += 2) r = e.x(l[a], l[a + 1]), i = e.y(l[a], l[a + 1]), l[a] = r, l[a + 1] = i;
                        return t
                    };
                if (e._g = A, e.type = A.win.SVGAngle || A.doc.implementation.hasFeature("http://www.w3.org/TR/SVG11/feature#BasicStructure", "1.1") ? "SVG" : "VML", "VML" == e.type) {
                    var Mt = A.doc.createElement("div"),
                        Lt;
                    if (Mt.innerHTML = '<v:shape adj="1"/>', Lt = Mt.firstChild, Lt.style.behavior = "url(#default#VML)", !Lt || "object" != typeof Lt.adj) return e.type = R;
                    Mt = null
                }
                e.svg = !(e.vml = "VML" == e.type), e._Paper = N, e.fn = M = N.prototype = e.prototype, e._id = 0, e._oid = 0, e.is = function (t, e) {
                    return e = O.call(e), "finite" == e ? !at[T](+t) : "array" == e ? t instanceof Array : "null" == e && null === t || e == typeof t && null !== t || "object" == e && t === Object(t) || "array" == e && Array.isArray && Array.isArray(t) || tt.call(t).slice(8, -1).toLowerCase() == e
                }, e.angle = function (t, r, i, n, a, s) {
                    if (null == a) {
                        var o = t - i,
                            l = r - n;
                        return o || l ? (180 + 180 * Y.atan2(-l, -o) / U + 360) % 360 : 0
                    }
                    return e.angle(t, r, a, s) - e.angle(i, n, a, s)
                }, e.rad = function (t) {
                    return t % 360 * U / 180
                }, e.deg = function (t) {
                    return Math.round(180 * t / U % 360 * 1e3) / 1e3
                }, e.snapTo = function (t, r, i) {
                    if (i = e.is(i, "finite") ? i : 10, e.is(t, Q)) {
                        for (var n = t.length; n--; )
                            if (H(t[n] - r) <= i) return t[n]
                        } else {
                            t = +t;
                            var a = r % t;
                            if (i > a) return r - a;
                            if (a > t - i) return r - a + t
                        }
                        return r
                    };
                    var zt = e.createUUID = function (t, e) {
                        return function () {
                            return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(t, e).toUpperCase()
                        }
                    } (/[xy]/g, function (t) {
                        var e = 16 * Y.random() | 0,
                        r = "x" == t ? e : 3 & e | 8;
                        return r.toString(16)
                    });
                    e.setWindow = function (r) {
                        t("raphael.setWindow", e, A.win, r), A.win = r, A.doc = A.win.document, e._engine.initWin && e._engine.initWin(A.win)
                    };
                    var Pt = function (t) {
                        if (e.vml) {
                            var r = /^\s+|\s+$/g,
                            i;
                            try {
                                var a = new ActiveXObject("htmlfile");
                                a.write("<body>"), a.close(), i = a.body
                            } catch (s) {
                                i = createPopup().document.body
                            }
                            var o = i.createTextRange();
                            Pt = n(function (t) {
                                try {
                                    i.style.color = j(t).replace(r, R);
                                    var e = o.queryCommandValue("ForeColor");
                                    return e = (255 & e) << 16 | 65280 & e | (16711680 & e) >>> 16, "#" + ("000000" + e.toString(16)).slice(-6)
                                } catch (n) {
                                    return "none"
                                }
                            })
                        } else {
                            var l = A.doc.createElement("i");
                            l.title = "Raphaël Colour Picker", l.style.display = "none", A.doc.body.appendChild(l), Pt = n(function (t) {
                                return l.style.color = t, A.doc.defaultView.getComputedStyle(l, R).getPropertyValue("color")
                            })
                        }
                        return Pt(t)
                    }, Ft = function () {
                        return "hsb(" + [this.h, this.s, this.b] + ")"
                    }, Rt = function () {
                        return "hsl(" + [this.h, this.s, this.l] + ")"
                    }, It = function () {
                        return this.hex
                    }, jt = function (t, r, i) {
                        if (null == r && e.is(t, "object") && "r" in t && "g" in t && "b" in t && (i = t.b, r = t.g, t = t.r), null == r && e.is(t, Z)) {
                            var n = e.getRGB(t);
                            t = n.r, r = n.g, i = n.b
                        }
                        return (t > 1 || r > 1 || i > 1) && (t /= 255, r /= 255, i /= 255), [t, r, i]
                    }, qt = function (t, r, i, n) {
                        t *= 255, r *= 255, i *= 255;
                        var a = {
                            r: t,
                            g: r,
                            b: i,
                            hex: e.rgb(t, r, i),
                            toString: It
                        };
                        return e.is(n, "finite") && (a.opacity = n), a
                    };
                    e.color = function (t) {
                        var r;
                        return e.is(t, "object") && "h" in t && "s" in t && "b" in t ? (r = e.hsb2rgb(t), t.r = r.r, t.g = r.g, t.b = r.b, t.hex = r.hex) : e.is(t, "object") && "h" in t && "s" in t && "l" in t ? (r = e.hsl2rgb(t), t.r = r.r, t.g = r.g, t.b = r.b, t.hex = r.hex) : (e.is(t, "string") && (t = e.getRGB(t)), e.is(t, "object") && "r" in t && "g" in t && "b" in t ? (r = e.rgb2hsl(t), t.h = r.h, t.s = r.s, t.l = r.l, r = e.rgb2hsb(t), t.v = r.b) : (t = {
                            hex: "none"
                        }, t.r = t.g = t.b = t.h = t.s = t.v = t.l = -1)), t.toString = It, t
                    }, e.hsb2rgb = function (t, e, r, i) {
                        this.is(t, "object") && "h" in t && "s" in t && "b" in t && (r = t.b, e = t.s, i = t.o, t = t.h), t *= 360;
                        var n, a, s, o, l;
                        return t = t % 360 / 60, l = r * e, o = l * (1 - H(t % 2 - 1)), n = a = s = r - l, t = ~ ~t, n += [l, o, 0, 0, o, l][t], a += [o, l, l, o, 0, 0][t], s += [0, 0, o, l, l, o][t], qt(n, a, s, i)
                    }, e.hsl2rgb = function (t, e, r, i) {
                        this.is(t, "object") && "h" in t && "s" in t && "l" in t && (r = t.l, e = t.s, t = t.h), (t > 1 || e > 1 || r > 1) && (t /= 360, e /= 100, r /= 100), t *= 360;
                        var n, a, s, o, l;
                        return t = t % 360 / 60, l = 2 * e * (.5 > r ? r : 1 - r), o = l * (1 - H(t % 2 - 1)), n = a = s = r - l / 2, t = ~ ~t, n += [l, o, 0, 0, o, l][t], a += [o, l, l, o, 0, 0][t], s += [0, 0, o, l, l, o][t], qt(n, a, s, i)
                    }, e.rgb2hsb = function (t, e, r) {
                        r = jt(t, e, r), t = r[0], e = r[1], r = r[2];
                        var i, n, a, s;
                        return a = W(t, e, r), s = a - G(t, e, r), i = 0 == s ? null : a == t ? (e - r) / s : a == e ? (r - t) / s + 2 : (t - e) / s + 4, i = (i + 360) % 6 * 60 / 360, n = 0 == s ? 0 : s / a, {
                            h: i,
                            s: n,
                            b: a,
                            toString: Ft
                        }
                    }, e.rgb2hsl = function (t, e, r) {
                        r = jt(t, e, r), t = r[0], e = r[1], r = r[2];
                        var i, n, a, s, o, l;
                        return s = W(t, e, r), o = G(t, e, r), l = s - o, i = 0 == l ? null : s == t ? (e - r) / l : s == e ? (r - t) / l + 2 : (t - e) / l + 4, i = (i + 360) % 6 * 60 / 360, a = (s + o) / 2, n = 0 == l ? 0 : .5 > a ? l / (2 * a) : l / (2 - 2 * a), {
                            h: i,
                            s: n,
                            l: a,
                            toString: Rt
                        }
                    }, e._path2string = function () {
                        return this.join(",").replace(vt, "$1")
                    };
                    var Dt = e._preload = function (t, e) {
                        var r = A.doc.createElement("img");
                        r.style.cssText = "position:absolute;left:-9999em;top:-9999em", r.onload = function () {
                            e.call(this), this.onload = null, A.doc.body.removeChild(this)
                        }, r.onerror = function () {
                            A.doc.body.removeChild(this)
                        }, A.doc.body.appendChild(r), r.src = t
                    };
                    e.getRGB = n(function (t) {
                        if (!t || (t = j(t)).indexOf("-") + 1) return {
                            r: -1,
                            g: -1,
                            b: -1,
                            hex: "none",
                            error: 1,
                            toString: a
                        };
                        if ("none" == t) return {
                            r: -1,
                            g: -1,
                            b: -1,
                            hex: "none",
                            toString: a
                        };
                        !(xt[T](t.toLowerCase().substring(0, 2)) || "#" == t.charAt()) && (t = Pt(t));
                        var r, i, n, s, o, l, h, u = t.match(nt);
                        return u ? (u[2] && (s = ut(u[2].substring(5), 16), n = ut(u[2].substring(3, 5), 16), i = ut(u[2].substring(1, 3), 16)), u[3] && (s = ut((l = u[3].charAt(3)) + l, 16), n = ut((l = u[3].charAt(2)) + l, 16), i = ut((l = u[3].charAt(1)) + l, 16)), u[4] && (h = u[4][q](gt), i = ht(h[0]), "%" == h[0].slice(-1) && (i *= 2.55), n = ht(h[1]), "%" == h[1].slice(-1) && (n *= 2.55), s = ht(h[2]), "%" == h[2].slice(-1) && (s *= 2.55), "rgba" == u[1].toLowerCase().slice(0, 4) && (o = ht(h[3])), h[3] && "%" == h[3].slice(-1) && (o /= 100)), u[5] ? (h = u[5][q](gt), i = ht(h[0]), "%" == h[0].slice(-1) && (i *= 2.55), n = ht(h[1]), "%" == h[1].slice(-1) && (n *= 2.55), s = ht(h[2]), "%" == h[2].slice(-1) && (s *= 2.55), ("deg" == h[0].slice(-3) || "°" == h[0].slice(-1)) && (i /= 360), "hsba" == u[1].toLowerCase().slice(0, 4) && (o = ht(h[3])), h[3] && "%" == h[3].slice(-1) && (o /= 100), e.hsb2rgb(i, n, s, o)) : u[6] ? (h = u[6][q](gt), i = ht(h[0]), "%" == h[0].slice(-1) && (i *= 2.55), n = ht(h[1]), "%" == h[1].slice(-1) && (n *= 2.55), s = ht(h[2]), "%" == h[2].slice(-1) && (s *= 2.55), ("deg" == h[0].slice(-3) || "°" == h[0].slice(-1)) && (i /= 360), "hsla" == u[1].toLowerCase().slice(0, 4) && (o = ht(h[3])), h[3] && "%" == h[3].slice(-1) && (o /= 100), e.hsl2rgb(i, n, s, o)) : (u = {
                            r: i,
                            g: n,
                            b: s,
                            toString: a
                        }, u.hex = "#" + (16777216 | s | n << 8 | i << 16).toString(16).slice(1), e.is(o, "finite") && (u.opacity = o), u)) : {
                            r: -1,
                            g: -1,
                            b: -1,
                            hex: "none",
                            error: 1,
                            toString: a
                        }
                    }, e), e.hsb = n(function (t, r, i) {
                        return e.hsb2rgb(t, r, i).hex
                    }), e.hsl = n(function (t, r, i) {
                        return e.hsl2rgb(t, r, i).hex
                    }), e.rgb = n(function (t, e, r) {
                        function i(t) {
                            return t + .5 | 0
                        }
                        return "#" + (16777216 | i(r) | i(e) << 8 | i(t) << 16).toString(16).slice(1)
                    }), e.getColor = function (t) {
                        var e = this.getColor.start = this.getColor.start || {
                            h: 0,
                            s: 1,
                            b: t || .75
                        }, r = this.hsb2rgb(e.h, e.s, e.b);
                        return e.h += .075, e.h > 1 && (e.h = 0, e.s -= .2, e.s <= 0 && (this.getColor.start = {
                            h: 0,
                            s: 1,
                            b: e.b
                        })), r.hex
                    }, e.getColor.reset = function () {
                        delete this.start
                    }, e.parsePathString = function (t) {
                        if (!t) return null;
                        var r = Vt(t);
                        if (r.arr) return Yt(r.arr);
                        var i = {
                            a: 7,
                            c: 6,
                            h: 1,
                            l: 2,
                            m: 2,
                            r: 4,
                            q: 4,
                            s: 4,
                            t: 2,
                            v: 1,
                            z: 0
                        }, n = [];
                        return e.is(t, Q) && e.is(t[0], Q) && (n = Yt(t)), n.length || j(t).replace(yt, function (t, e, r) {
                            var a = [],
                            s = e.toLowerCase();
                            if (r.replace(bt, function (t, e) {
                                e && a.push(+e)
                            }), "m" == s && a.length > 2 && (n.push([e][P](a.splice(0, 2))), s = "l", e = "m" == e ? "l" : "L"), "r" == s) n.push([e][P](a));
                            else
                                for (; a.length >= i[s] && (n.push([e][P](a.splice(0, i[s]))), i[s]); );
                        }), n.toString = e._path2string, r.arr = Yt(n), n
                    }, e.parseTransformString = n(function (t) {
                        if (!t) return null;
                        var r = {
                            r: 3,
                            s: 4,
                            t: 2,
                            m: 6
                        }, i = [];
                        return e.is(t, Q) && e.is(t[0], Q) && (i = Yt(t)), i.length || j(t).replace(mt, function (t, e, r) {
                            var n = [],
                            a = O.call(e);
                            r.replace(bt, function (t, e) {
                                e && n.push(+e)
                            }), i.push([e][P](n))
                        }), i.toString = e._path2string, i
                    });
                    var Vt = function (t) {
                        var e = Vt.ps = Vt.ps || {};
                        return e[t] ? e[t].sleep = 100 : e[t] = {
                            sleep: 100
                        }, setTimeout(function () {
                            for (var r in e) e[T](r) && r != t && (e[r].sleep--, !e[r].sleep && delete e[r])
                        }), e[t]
                    };
                    e.findDotsAtSegment = function (t, e, r, i, n, a, s, o, l) {
                        var h = 1 - l,
                        u = X(h, 3),
                        c = X(h, 2),
                        f = l * l,
                        p = f * l,
                        d = u * t + 3 * c * l * r + 3 * h * l * l * n + p * s,
                        g = u * e + 3 * c * l * i + 3 * h * l * l * a + p * o,
                        x = t + 2 * l * (r - t) + f * (n - 2 * r + t),
                        v = e + 2 * l * (i - e) + f * (a - 2 * i + e),
                        y = r + 2 * l * (n - r) + f * (s - 2 * n + r),
                        m = i + 2 * l * (a - i) + f * (o - 2 * a + i),
                        b = h * t + l * r,
                        _ = h * e + l * i,
                        w = h * n + l * s,
                        k = h * a + l * o,
                        B = 90 - 180 * Y.atan2(x - y, v - m) / U;
                        return (x > y || m > v) && (B += 180), {
                            x: d,
                            y: g,
                            m: {
                                x: x,
                                y: v
                            },
                            n: {
                                x: y,
                                y: m
                            },
                            start: {
                                x: b,
                                y: _
                            },
                            end: {
                                x: w,
                                y: k
                            },
                            alpha: B
                        }
                    }, e.bezierBBox = function (t, r, i, n, a, s, o, l) {
                        e.is(t, "array") || (t = [t, r, i, n, a, s, o, l]);
                        var h = Zt.apply(null, t);
                        return {
                            x: h.min.x,
                            y: h.min.y,
                            x2: h.max.x,
                            y2: h.max.y,
                            width: h.max.x - h.min.x,
                            height: h.max.y - h.min.y
                        }
                    }, e.isPointInsideBBox = function (t, e, r) {
                        return e >= t.x && e <= t.x2 && r >= t.y && r <= t.y2
                    }, e.isBBoxIntersect = function (t, r) {
                        var i = e.isPointInsideBBox;
                        return i(r, t.x, t.y) || i(r, t.x2, t.y) || i(r, t.x, t.y2) || i(r, t.x2, t.y2) || i(t, r.x, r.y) || i(t, r.x2, r.y) || i(t, r.x, r.y2) || i(t, r.x2, r.y2) || (t.x < r.x2 && t.x > r.x || r.x < t.x2 && r.x > t.x) && (t.y < r.y2 && t.y > r.y || r.y < t.y2 && r.y > t.y)
                    }, e.pathIntersection = function (t, e) {
                        return d(t, e)
                    }, e.pathIntersectionNumber = function (t, e) {
                        return d(t, e, 1)
                    }, e.isPointInsidePath = function (t, r, i) {
                        var n = e.pathBBox(t);
                        return e.isPointInsideBBox(n, r, i) && d(t, [
                        ["M", r, i],
                        ["H", n.x2 + 10]
                    ], 1) % 2 == 1
                    }, e._removedFactory = function (e) {
                        return function () {
                            t("raphael.log", null, "Raphaël: you are calling to method “" + e + "” of removed object", e)
                        }
                    };
                    var Ot = e.pathBBox = function (t) {
                        var e = Vt(t);
                        if (e.bbox) return r(e.bbox);
                        if (!t) return {
                            x: 0,
                            y: 0,
                            width: 0,
                            height: 0,
                            x2: 0,
                            y2: 0
                        };
                        t = Qt(t);
                        for (var i = 0, n = 0, a = [], s = [], o, l = 0, h = t.length; h > l; l++)
                            if (o = t[l], "M" == o[0]) i = o[1], n = o[2], a.push(i), s.push(n);
                            else {
                                var u = Zt(i, n, o[1], o[2], o[3], o[4], o[5], o[6]);
                                a = a[P](u.min.x, u.max.x), s = s[P](u.min.y, u.max.y), i = o[5], n = o[6]
                            }
                        var c = G[z](0, a),
                        f = G[z](0, s),
                        p = W[z](0, a),
                        d = W[z](0, s),
                        g = p - c,
                        x = d - f,
                        v = {
                            x: c,
                            y: f,
                            x2: p,
                            y2: d,
                            width: g,
                            height: x,
                            cx: c + g / 2,
                            cy: f + x / 2
                        };
                        return e.bbox = r(v), v
                    }, Yt = function (t) {
                        var i = r(t);
                        return i.toString = e._path2string, i
                    }, Wt = e._pathToRelative = function (t) {
                        var r = Vt(t);
                        if (r.rel) return Yt(r.rel);
                        e.is(t, Q) && e.is(t && t[0], Q) || (t = e.parsePathString(t));
                        var i = [],
                            n = 0,
                            a = 0,
                            s = 0,
                            o = 0,
                            l = 0;
                        "M" == t[0][0] && (n = t[0][1], a = t[0][2], s = n, o = a, l++, i.push(["M", n, a]));
                        for (var h = l, u = t.length; u > h; h++) {
                            var c = i[h] = [],
                                f = t[h];
                            if (f[0] != O.call(f[0])) switch (c[0] = O.call(f[0]), c[0]) {
                                case "a":
                                    c[1] = f[1], c[2] = f[2], c[3] = f[3], c[4] = f[4], c[5] = f[5], c[6] = +(f[6] - n).toFixed(3), c[7] = +(f[7] - a).toFixed(3);
                                    break;
                                case "v":
                                    c[1] = +(f[1] - a).toFixed(3);
                                    break;
                                case "m":
                                    s = f[1], o = f[2];
                                default:
                                    for (var p = 1, d = f.length; d > p; p++) c[p] = +(f[p] - (p % 2 ? n : a)).toFixed(3)
                            } else {
                                c = i[h] = [], "m" == f[0] && (s = f[1] + n, o = f[2] + a);
                                for (var g = 0, x = f.length; x > g; g++) i[h][g] = f[g]
                            }
                            var v = i[h].length;
                            switch (i[h][0]) {
                                case "z":
                                    n = s, a = o;
                                    break;
                                case "h":
                                    n += +i[h][v - 1];
                                    break;
                                case "v":
                                    a += +i[h][v - 1];
                                    break;
                                default:
                                    n += +i[h][v - 2], a += +i[h][v - 1]
                            }
                        }
                        return i.toString = e._path2string, r.rel = Yt(i), i
                    }, Gt = e._pathToAbsolute = function (t) {
                        var r = Vt(t);
                        if (r.abs) return Yt(r.abs);
                        if (e.is(t, Q) && e.is(t && t[0], Q) || (t = e.parsePathString(t)), !t || !t.length) return [["M", 0, 0]];
                        var i = [],
                            n = 0,
                            a = 0,
                            o = 0,
                            l = 0,
                            h = 0;
                        "M" == t[0][0] && (n = +t[0][1], a = +t[0][2], o = n, l = a, h++, i[0] = ["M", n, a]);
                        for (var u = 3 == t.length && "M" == t[0][0] && "R" == t[1][0].toUpperCase() && "Z" == t[2][0].toUpperCase(), c, f, p = h, d = t.length; d > p; p++) {
                            if (i.push(c = []), f = t[p], f[0] != ct.call(f[0])) switch (c[0] = ct.call(f[0]), c[0]) {
                                case "A":
                                    c[1] = f[1], c[2] = f[2], c[3] = f[3], c[4] = f[4], c[5] = f[5], c[6] = +(f[6] + n), c[7] = +(f[7] + a);
                                    break;
                                case "V":
                                    c[1] = +f[1] + a;
                                    break;
                                case "H":
                                    c[1] = +f[1] + n;
                                    break;
                                case "R":
                                    for (var g = [n, a][P](f.slice(1)), x = 2, v = g.length; v > x; x++) g[x] = +g[x] + n, g[++x] = +g[x] + a;
                                    i.pop(), i = i[P](s(g, u));
                                    break;
                                case "M":
                                    o = +f[1] + n, l = +f[2] + a;
                                default:
                                    for (x = 1, v = f.length; v > x; x++) c[x] = +f[x] + (x % 2 ? n : a)
                            } else if ("R" == f[0]) g = [n, a][P](f.slice(1)), i.pop(), i = i[P](s(g, u)), c = ["R"][P](f.slice(-2));
                            else
                                for (var y = 0, m = f.length; m > y; y++) c[y] = f[y];
                            switch (c[0]) {
                                case "Z":
                                    n = o, a = l;
                                    break;
                                case "H":
                                    n = c[1];
                                    break;
                                case "V":
                                    a = c[1];
                                    break;
                                case "M":
                                    o = c[c.length - 2], l = c[c.length - 1];
                                default:
                                    n = c[c.length - 2], a = c[c.length - 1]
                            }
                        }
                        return i.toString = e._path2string, r.abs = Yt(i), i
                    }, Ht = function (t, e, r, i) {
                        return [t, e, r, i, r, i]
                    }, Xt = function (t, e, r, i, n, a) {
                        var s = 1 / 3,
                            o = 2 / 3;
                        return [s * t + o * r, s * e + o * i, s * n + o * r, s * a + o * i, n, a]
                    }, Ut = function (t, e, r, i, a, s, o, l, h, u) {
                        var c = 120 * U / 180,
                            f = U / 180 * (+a || 0),
                            p = [],
                            d, g = n(function (t, e, r) {
                                var i = t * Y.cos(r) - e * Y.sin(r),
                                    n = t * Y.sin(r) + e * Y.cos(r);
                                return {
                                    x: i,
                                    y: n
                                }
                            });
                        if (u) S = u[0], T = u[1], B = u[2], C = u[3];
                        else {
                            d = g(t, e, -f), t = d.x, e = d.y, d = g(l, h, -f), l = d.x, h = d.y;
                            var x = Y.cos(U / 180 * a),
                                v = Y.sin(U / 180 * a),
                                y = (t - l) / 2,
                                m = (e - h) / 2,
                                b = y * y / (r * r) + m * m / (i * i);
                            b > 1 && (b = Y.sqrt(b), r = b * r, i = b * i);
                            var _ = r * r,
                                w = i * i,
                                k = (s == o ? -1 : 1) * Y.sqrt(H((_ * w - _ * m * m - w * y * y) / (_ * m * m + w * y * y))),
                                B = k * r * m / i + (t + l) / 2,
                                C = k * -i * y / r + (e + h) / 2,
                                S = Y.asin(((e - C) / i).toFixed(9)),
                                T = Y.asin(((h - C) / i).toFixed(9));
                            S = B > t ? U - S : S, T = B > l ? U - T : T, 0 > S && (S = 2 * U + S), 0 > T && (T = 2 * U + T), o && S > T && (S -= 2 * U), !o && T > S && (T -= 2 * U)
                        }
                        var A = T - S;
                        if (H(A) > c) {
                            var E = T,
                                N = l,
                                M = h;
                            T = S + c * (o && T > S ? 1 : -1), l = B + r * Y.cos(T), h = C + i * Y.sin(T), p = Ut(l, h, r, i, a, 0, o, N, M, [T, E, B, C])
                        }
                        A = T - S;
                        var L = Y.cos(S),
                            z = Y.sin(S),
                            F = Y.cos(T),
                            R = Y.sin(T),
                            I = Y.tan(A / 4),
                            j = 4 / 3 * r * I,
                            D = 4 / 3 * i * I,
                            V = [t, e],
                            O = [t + j * z, e - D * L],
                            W = [l + j * R, h - D * F],
                            G = [l, h];
                        if (O[0] = 2 * V[0] - O[0], O[1] = 2 * V[1] - O[1], u) return [O, W, G][P](p);
                        p = [O, W, G][P](p).join()[q](",");
                        for (var X = [], $ = 0, Z = p.length; Z > $; $++) X[$] = $ % 2 ? g(p[$ - 1], p[$], f).y : g(p[$], p[$ + 1], f).x;
                        return X
                    }, $t = function (t, e, r, i, n, a, s, o, l) {
                        var h = 1 - l;
                        return {
                            x: X(h, 3) * t + 3 * X(h, 2) * l * r + 3 * h * l * l * n + X(l, 3) * s,
                            y: X(h, 3) * e + 3 * X(h, 2) * l * i + 3 * h * l * l * a + X(l, 3) * o
                        }
                    }, Zt = n(function (t, e, r, i, n, a, s, o) {
                        var l = n - 2 * r + t - (s - 2 * n + r),
                            h = 2 * (r - t) - 2 * (n - r),
                            u = t - r,
                            c = (-h + Y.sqrt(h * h - 4 * l * u)) / 2 / l,
                            f = (-h - Y.sqrt(h * h - 4 * l * u)) / 2 / l,
                            p = [e, o],
                            d = [t, s],
                            g;
                        return H(c) > "1e12" && (c = .5), H(f) > "1e12" && (f = .5), c > 0 && 1 > c && (g = $t(t, e, r, i, n, a, s, o, c), d.push(g.x), p.push(g.y)), f > 0 && 1 > f && (g = $t(t, e, r, i, n, a, s, o, f), d.push(g.x), p.push(g.y)), l = a - 2 * i + e - (o - 2 * a + i), h = 2 * (i - e) - 2 * (a - i), u = e - i, c = (-h + Y.sqrt(h * h - 4 * l * u)) / 2 / l, f = (-h - Y.sqrt(h * h - 4 * l * u)) / 2 / l, H(c) > "1e12" && (c = .5), H(f) > "1e12" && (f = .5), c > 0 && 1 > c && (g = $t(t, e, r, i, n, a, s, o, c), d.push(g.x), p.push(g.y)), f > 0 && 1 > f && (g = $t(t, e, r, i, n, a, s, o, f), d.push(g.x), p.push(g.y)), {
                            min: {
                                x: G[z](0, d),
                                y: G[z](0, p)
                            },
                            max: {
                                x: W[z](0, d),
                                y: W[z](0, p)
                            }
                        }
                    }),
                    Qt = e._path2curve = n(function (t, e) {
                        var r = !e && Vt(t);
                        if (!e && r.curve) return Yt(r.curve);
                        for (var i = Gt(t), n = e && Gt(e), a = {
                            x: 0,
                            y: 0,
                            bx: 0,
                            by: 0,
                            X: 0,
                            Y: 0,
                            qx: null,
                            qy: null
                        }, s = {
                            x: 0,
                            y: 0,
                            bx: 0,
                            by: 0,
                            X: 0,
                            Y: 0,
                            qx: null,
                            qy: null
                        }, o = (function (t, e, r) {
                            var i, n, a = {
                                T: 1,
                                Q: 1
                            };
                            if (!t) return ["C", e.x, e.y, e.x, e.y, e.x, e.y];
                            switch (!(t[0] in a) && (e.qx = e.qy = null), t[0]) {
                                case "M":
                                    e.X = t[1], e.Y = t[2];
                                    break;
                                case "A":
                                    t = ["C"][P](Ut[z](0, [e.x, e.y][P](t.slice(1))));
                                    break;
                                case "S":
                                    "C" == r || "S" == r ? (i = 2 * e.x - e.bx, n = 2 * e.y - e.by) : (i = e.x, n = e.y), t = ["C", i, n][P](t.slice(1));
                                    break;
                                case "T":
                                    "Q" == r || "T" == r ? (e.qx = 2 * e.x - e.qx, e.qy = 2 * e.y - e.qy) : (e.qx = e.x, e.qy = e.y), t = ["C"][P](Xt(e.x, e.y, e.qx, e.qy, t[1], t[2]));
                                    break;
                                case "Q":
                                    e.qx = t[1], e.qy = t[2], t = ["C"][P](Xt(e.x, e.y, t[1], t[2], t[3], t[4]));
                                    break;
                                case "L":
                                    t = ["C"][P](Ht(e.x, e.y, t[1], t[2]));
                                    break;
                                case "H":
                                    t = ["C"][P](Ht(e.x, e.y, t[1], e.y));
                                    break;
                                case "V":
                                    t = ["C"][P](Ht(e.x, e.y, e.x, t[1]));
                                    break;
                                case "Z":
                                    t = ["C"][P](Ht(e.x, e.y, e.X, e.Y))
                            }
                            return t
                        }), l = function (t, e) {
                            if (t[e].length > 7) {
                                t[e].shift();
                                for (var r = t[e]; r.length; ) u[e] = "A", n && (c[e] = "A"), t.splice(e++, 0, ["C"][P](r.splice(0, 6)));
                                t.splice(e, 1), g = W(i.length, n && n.length || 0)
                            }
                        }, h = function (t, e, r, a, s) {
                            t && e && "M" == t[s][0] && "M" != e[s][0] && (e.splice(s, 0, ["M", a.x, a.y]), r.bx = 0, r.by = 0, r.x = t[s][1], r.y = t[s][2], g = W(i.length, n && n.length || 0))
                        }, u = [], c = [], f = "", p = "", d = 0, g = W(i.length, n && n.length || 0); g > d; d++) {
                            i[d] && (f = i[d][0]), "C" != f && (u[d] = f, d && (p = u[d - 1])), i[d] = o(i[d], a, p), "A" != u[d] && "C" == f && (u[d] = "C"), l(i, d), n && (n[d] && (f = n[d][0]), "C" != f && (c[d] = f, d && (p = c[d - 1])), n[d] = o(n[d], s, p), "A" != c[d] && "C" == f && (c[d] = "C"), l(n, d)), h(i, n, a, s, d), h(n, i, s, a, d);
                            var x = i[d],
                                v = n && n[d],
                                y = x.length,
                                m = n && v.length;
                            a.x = x[y - 2], a.y = x[y - 1], a.bx = ht(x[y - 4]) || a.x, a.by = ht(x[y - 3]) || a.y, s.bx = n && (ht(v[m - 4]) || s.x), s.by = n && (ht(v[m - 3]) || s.y), s.x = n && v[m - 2], s.y = n && v[m - 1]
                        }
                        return n || (r.curve = Yt(i)), n ? [i, n] : i
                    }, null, Yt),
                    Jt = e._parseDots = n(function (t) {
                        for (var r = [], i = 0, n = t.length; n > i; i++) {
                            var a = {}, s = t[i].match(/^([^:]*):?([\d\.]*)/);
                            if (a.color = e.getRGB(s[1]), a.color.error) return null;
                            a.opacity = a.color.opacity, a.color = a.color.hex, s[2] && (a.offset = s[2] + "%"), r.push(a)
                        }
                        for (i = 1, n = r.length - 1; n > i; i++)
                            if (!r[i].offset) {
                                for (var o = ht(r[i - 1].offset || 0), l = 0, h = i + 1; n > h; h++)
                                    if (r[h].offset) {
                                        l = r[h].offset;
                                        break
                                    }
                                l || (l = 100, h = n), l = ht(l);
                                for (var u = (l - o) / (h - i + 1); h > i; i++) o += u, r[i].offset = o + "%"
                            }
                        return r
                    }),
                    Kt = e._tear = function (t, e) {
                        t == e.top && (e.top = t.prev), t == e.bottom && (e.bottom = t.next), t.next && (t.next.prev = t.prev), t.prev && (t.prev.next = t.next)
                    }, te = e._tofront = function (t, e) {
                        e.top !== t && (Kt(t, e), t.next = null, t.prev = e.top, e.top.next = t, e.top = t)
                    }, ee = e._toback = function (t, e) {
                        e.bottom !== t && (Kt(t, e), t.next = e.bottom, t.prev = null, e.bottom.prev = t, e.bottom = t)
                    }, re = e._insertafter = function (t, e, r) {
                        Kt(t, r), e == r.top && (r.top = t), e.next && (e.next.prev = t), t.next = e.next, t.prev = e, e.next = t
                    }, ie = e._insertbefore = function (t, e, r) {
                        Kt(t, r), e == r.bottom && (r.bottom = t), e.prev && (e.prev.next = t), t.prev = e.prev, e.prev = t, t.next = e
                    }, ne = e.toMatrix = function (t, e) {
                        var r = Ot(t),
                            i = {
                                _: {
                                    transform: R
                                },
                                getBBox: function () {
                                    return r
                                }
                            };
                        return se(i, e), i.matrix
                    }, ae = e.transformPath = function (t, e) {
                        return Nt(t, ne(t, e))
                    }, se = e._extractTransform = function (t, r) {
                        if (null == r) return t._.transform;
                        r = j(r).replace(/\.{3}|\u2026/g, t._.transform || R);
                        var i = e.parseTransformString(r),
                            n = 0,
                            a = 0,
                            s = 0,
                            o = 1,
                            l = 1,
                            h = t._,
                            u = new g;
                        if (h.transform = i || [], i)
                            for (var c = 0, f = i.length; f > c; c++) {
                                var p = i[c],
                                    d = p.length,
                                    x = j(p[0]).toLowerCase(),
                                    v = p[0] != x,
                                    y = v ? u.invert() : 0,
                                    m, b, _, w, k;
                                "t" == x && 3 == d ? v ? (m = y.x(0, 0), b = y.y(0, 0), _ = y.x(p[1], p[2]), w = y.y(p[1], p[2]), u.translate(_ - m, w - b)) : u.translate(p[1], p[2]) : "r" == x ? 2 == d ? (k = k || t.getBBox(1), u.rotate(p[1], k.x + k.width / 2, k.y + k.height / 2), n += p[1]) : 4 == d && (v ? (_ = y.x(p[2], p[3]), w = y.y(p[2], p[3]), u.rotate(p[1], _, w)) : u.rotate(p[1], p[2], p[3]), n += p[1]) : "s" == x ? 2 == d || 3 == d ? (k = k || t.getBBox(1), u.scale(p[1], p[d - 1], k.x + k.width / 2, k.y + k.height / 2), o *= p[1], l *= p[d - 1]) : 5 == d && (v ? (_ = y.x(p[3], p[4]), w = y.y(p[3], p[4]), u.scale(p[1], p[2], _, w)) : u.scale(p[1], p[2], p[3], p[4]), o *= p[1], l *= p[2]) : "m" == x && 7 == d && u.add(p[1], p[2], p[3], p[4], p[5], p[6]), h.dirtyT = 1, t.matrix = u
                            }
                        t.matrix = u, h.sx = o, h.sy = l, h.deg = n, h.dx = a = u.e, h.dy = s = u.f, 1 == o && 1 == l && !n && h.bbox ? (h.bbox.x += +a, h.bbox.y += +s) : h.dirtyT = 1
                    }, oe = function (t) {
                        var e = t[0];
                        switch (e.toLowerCase()) {
                            case "t":
                                return [e, 0, 0];
                            case "m":
                                return [e, 1, 0, 0, 1, 0, 0];
                            case "r":
                                return 4 == t.length ? [e, 0, t[2], t[3]] : [e, 0];
                            case "s":
                                return 5 == t.length ? [e, 1, 1, t[3], t[4]] : 3 == t.length ? [e, 1, 1] : [e, 1]
                        }
                    }, le = e._equaliseTransform = function (t, r) {
                        r = j(r).replace(/\.{3}|\u2026/g, t), t = e.parseTransformString(t) || [], r = e.parseTransformString(r) || [];
                        for (var i = W(t.length, r.length), n = [], a = [], s = 0, o, l, h, u; i > s; s++) {
                            if (h = t[s] || oe(r[s]), u = r[s] || oe(h), h[0] != u[0] || "r" == h[0].toLowerCase() && (h[2] != u[2] || h[3] != u[3]) || "s" == h[0].toLowerCase() && (h[3] != u[3] || h[4] != u[4])) return;
                            for (n[s] = [], a[s] = [], o = 0, l = W(h.length, u.length); l > o; o++) o in h && (n[s][o] = h[o]), o in u && (a[s][o] = u[o])
                        }
                        return {
                            from: n,
                            to: a
                        }
                    };
                    e._getContainer = function (t, r, i, n) {
                        var a;
                        return a = null != n || e.is(t, "object") ? t : A.doc.getElementById(t), null != a ? a.tagName ? null == r ? {
                            container: a,
                            width: a.style.pixelWidth || a.offsetWidth,
                            height: a.style.pixelHeight || a.offsetHeight
                        } : {
                            container: a,
                            width: r,
                            height: i
                        } : {
                            container: 1,
                            x: t,
                            y: r,
                            width: i,
                            height: n
                        } : void 0
                    }, e.pathToRelative = Wt, e._engine = {}, e.path2curve = Qt, e.matrix = function (t, e, r, i, n, a) {
                        return new g(t, e, r, i, n, a)
                    },
                function (t) {
                    function r(t) {
                        return t[0] * t[0] + t[1] * t[1]
                    }

                    function i(t) {
                        var e = Y.sqrt(r(t));
                        t[0] && (t[0] /= e), t[1] && (t[1] /= e)
                    }
                    t.add = function (t, e, r, i, n, a) {
                        var s = [
                            [],
                            [],
                            []
                        ],
                            o = [
                                [this.a, this.c, this.e],
                                [this.b, this.d, this.f],
                                [0, 0, 1]
                            ],
                            l = [
                                [t, r, n],
                                [e, i, a],
                                [0, 0, 1]
                            ],
                            h, u, c, f;
                        for (t && t instanceof g && (l = [
                            [t.a, t.c, t.e],
                            [t.b, t.d, t.f],
                            [0, 0, 1]
                        ]), h = 0; 3 > h; h++)
                            for (u = 0; 3 > u; u++) {
                                for (f = 0, c = 0; 3 > c; c++) f += o[h][c] * l[c][u];
                                s[h][u] = f
                            }
                        this.a = s[0][0], this.b = s[1][0], this.c = s[0][1], this.d = s[1][1], this.e = s[0][2], this.f = s[1][2]
                    }, t.invert = function () {
                        var t = this,
                            e = t.a * t.d - t.b * t.c;
                        return new g(t.d / e, -t.b / e, -t.c / e, t.a / e, (t.c * t.f - t.d * t.e) / e, (t.b * t.e - t.a * t.f) / e)
                    }, t.clone = function () {
                        return new g(this.a, this.b, this.c, this.d, this.e, this.f)
                    }, t.translate = function (t, e) {
                        this.add(1, 0, 0, 1, t, e)
                    }, t.scale = function (t, e, r, i) {
                        null == e && (e = t), (r || i) && this.add(1, 0, 0, 1, r, i), this.add(t, 0, 0, e, 0, 0), (r || i) && this.add(1, 0, 0, 1, -r, -i)
                    }, t.rotate = function (t, r, i) {
                        t = e.rad(t), r = r || 0, i = i || 0;
                        var n = +Y.cos(t).toFixed(9),
                            a = +Y.sin(t).toFixed(9);
                        this.add(n, a, -a, n, r, i), this.add(1, 0, 0, 1, -r, -i)
                    }, t.x = function (t, e) {
                        return t * this.a + e * this.c + this.e
                    }, t.y = function (t, e) {
                        return t * this.b + e * this.d + this.f
                    }, t.get = function (t) {
                        return +this[j.fromCharCode(97 + t)].toFixed(4)
                    }, t.toString = function () {
                        return e.svg ? "matrix(" + [this.get(0), this.get(1), this.get(2), this.get(3), this.get(4), this.get(5)].join() + ")" : [this.get(0), this.get(2), this.get(1), this.get(3), 0, 0].join()
                    }, t.toFilter = function () {
                        return "progid:DXImageTransform.Microsoft.Matrix(M11=" + this.get(0) + ", M12=" + this.get(2) + ", M21=" + this.get(1) + ", M22=" + this.get(3) + ", Dx=" + this.get(4) + ", Dy=" + this.get(5) + ", sizingmethod='auto expand')"
                    }, t.offset = function () {
                        return [this.e.toFixed(4), this.f.toFixed(4)]
                    }, t.split = function () {
                        var t = {};
                        t.dx = this.e, t.dy = this.f;
                        var n = [
                            [this.a, this.c],
                            [this.b, this.d]
                        ];
                        t.scalex = Y.sqrt(r(n[0])), i(n[0]), t.shear = n[0][0] * n[1][0] + n[0][1] * n[1][1], n[1] = [n[1][0] - n[0][0] * t.shear, n[1][1] - n[0][1] * t.shear], t.scaley = Y.sqrt(r(n[1])), i(n[1]), t.shear /= t.scaley;
                        var a = -n[0][1],
                            s = n[1][1];
                        return 0 > s ? (t.rotate = e.deg(Y.acos(s)), 0 > a && (t.rotate = 360 - t.rotate)) : t.rotate = e.deg(Y.asin(a)), t.isSimple = !(+t.shear.toFixed(9) || t.scalex.toFixed(9) != t.scaley.toFixed(9) && t.rotate), t.isSuperSimple = ! +t.shear.toFixed(9) && t.scalex.toFixed(9) == t.scaley.toFixed(9) && !t.rotate, t.noRotation = ! +t.shear.toFixed(9) && !t.rotate, t
                    }, t.toTransformString = function (t) {
                        var e = t || this[q]();
                        return e.isSimple ? (e.scalex = +e.scalex.toFixed(4), e.scaley = +e.scaley.toFixed(4), e.rotate = +e.rotate.toFixed(4), (e.dx || e.dy ? "t" + [e.dx, e.dy] : R) + (1 != e.scalex || 1 != e.scaley ? "s" + [e.scalex, e.scaley, 0, 0] : R) + (e.rotate ? "r" + [e.rotate, 0, 0] : R)) : "m" + [this.get(0), this.get(1), this.get(2), this.get(3), this.get(4), this.get(5)]
                    }
                } (g.prototype);
                    for (var he = function () {
                        this.returnValue = !1
                    }, ue = function () {
                        return this.originalEvent.preventDefault()
                    }, ce = function () {
                        this.cancelBubble = !0
                    }, fe = function () {
                        return this.originalEvent.stopPropagation()
                    }, pe = function (t) {
                        var e = A.doc.documentElement.scrollTop || A.doc.body.scrollTop,
                            r = A.doc.documentElement.scrollLeft || A.doc.body.scrollLeft;
                        return {
                            x: t.clientX + r,
                            y: t.clientY + e
                        }
                    }, de = function () {
                        return A.doc.addEventListener ? function (t, e, r, i) {
                            var n = function (t) {
                                var e = pe(t);
                                return r.call(i, t, e.x, e.y)
                            };
                            if (t.addEventListener(e, n, !1), F && V[e]) {
                                var a = function (e) {
                                    for (var n = pe(e), a = e, s = 0, o = e.targetTouches && e.targetTouches.length; o > s; s++)
                                        if (e.targetTouches[s].target == t) {
                                            e = e.targetTouches[s], e.originalEvent = a, e.preventDefault = ue, e.stopPropagation = fe;
                                            break
                                        }
                                    return r.call(i, e, n.x, n.y)
                                };
                                t.addEventListener(V[e], a, !1)
                            }
                            return function () {
                                return t.removeEventListener(e, n, !1), F && V[e] && t.removeEventListener(V[e], a, !1), !0
                            }
                        } : A.doc.attachEvent ? function (t, e, r, i) {
                            var n = function (t) {
                                t = t || A.win.event;
                                var e = A.doc.documentElement.scrollTop || A.doc.body.scrollTop,
                                    n = A.doc.documentElement.scrollLeft || A.doc.body.scrollLeft,
                                    a = t.clientX + n,
                                    s = t.clientY + e;
                                return t.preventDefault = t.preventDefault || he, t.stopPropagation = t.stopPropagation || ce, r.call(i, t, a, s)
                            };
                            t.attachEvent("on" + e, n);
                            var a = function () {
                                return t.detachEvent("on" + e, n), !0
                            };
                            return a
                        } : void 0
                    } (), ge = [], xe = function (e) {
                        for (var r = e.clientX, i = e.clientY, n = A.doc.documentElement.scrollTop || A.doc.body.scrollTop, a = A.doc.documentElement.scrollLeft || A.doc.body.scrollLeft, s, o = ge.length; o--; ) {
                            if (s = ge[o], F && e.touches) {
                                for (var l = e.touches.length, h; l--; )
                                    if (h = e.touches[l], h.identifier == s.el._drag.id) {
                                        r = h.clientX, i = h.clientY, (e.originalEvent ? e.originalEvent : e).preventDefault();
                                        break
                                    }
                            } else e.preventDefault();
                            var u = s.el.node,
                                c, f = u.nextSibling,
                                p = u.parentNode,
                                d = u.style.display;
                            A.win.opera && p.removeChild(u), u.style.display = "none", c = s.el.paper.getElementByPoint(r, i), u.style.display = d, A.win.opera && (f ? p.insertBefore(u, f) : p.appendChild(u)), c && t("raphael.drag.over." + s.el.id, s.el, c), r += a, i += n, t("raphael.drag.move." + s.el.id, s.move_scope || s.el, r - s.el._drag.x, i - s.el._drag.y, r, i, e)
                        }
                    }, ve = function (r) {
                        e.unmousemove(xe).unmouseup(ve);
                        for (var i = ge.length, n; i--; ) n = ge[i], n.el._drag = {}, t("raphael.drag.end." + n.el.id, n.end_scope || n.start_scope || n.move_scope || n.el, r);
                        ge = []
                    }, ye = e.el = {}, me = D.length; me--; ) !function (t) {
                        e[t] = ye[t] = function (r, i) {
                            return e.is(r, "function") && (this.events = this.events || [], this.events.push({
                                name: t,
                                f: r,
                                unbind: de(this.shape || this.node || A.doc, t, r, i || this)
                            })), this
                        }, e["un" + t] = ye["un" + t] = function (r) {
                            for (var i = this.events || [], n = i.length; n--; ) i[n].name != t || !e.is(r, "undefined") && i[n].f != r || (i[n].unbind(), i.splice(n, 1), !i.length && delete this.events);
                            return this
                        }
                    } (D[me]);
                    ye.data = function (r, i) {
                        var n = wt[this.id] = wt[this.id] || {};
                        if (0 == arguments.length) return n;
                        if (1 == arguments.length) {
                            if (e.is(r, "object")) {
                                for (var a in r) r[T](a) && this.data(a, r[a]);
                                return this
                            }
                            return t("raphael.data.get." + this.id, this, n[r], r), n[r]
                        }
                        return n[r] = i, t("raphael.data.set." + this.id, this, i, r), this
                    }, ye.removeData = function (t) {
                        return null == t ? wt[this.id] = {} : wt[this.id] && delete wt[this.id][t], this
                    }, ye.getData = function () {
                        return r(wt[this.id] || {})
                    }, ye.hover = function (t, e, r, i) {
                        return this.mouseover(t, r).mouseout(e, i || r)
                    }, ye.unhover = function (t, e) {
                        return this.unmouseover(t).unmouseout(e)
                    };
                    var be = [];
                    ye.drag = function (r, i, n, a, s, o) {
                        function l(l) {
                            (l.originalEvent || l).preventDefault();
                            var h = l.clientX,
                            u = l.clientY,
                            c = A.doc.documentElement.scrollTop || A.doc.body.scrollTop,
                            f = A.doc.documentElement.scrollLeft || A.doc.body.scrollLeft;
                            if (this._drag.id = l.identifier, F && l.touches)
                                for (var p = l.touches.length, d; p--; )
                                    if (d = l.touches[p], this._drag.id = d.identifier, d.identifier == this._drag.id) {
                                        h = d.clientX, u = d.clientY;
                                        break
                                    }
                            this._drag.x = h + f, this._drag.y = u + c, !ge.length && e.mousemove(xe).mouseup(ve), ge.push({
                                el: this,
                                move_scope: a,
                                start_scope: s,
                                end_scope: o
                            }), i && t.on("raphael.drag.start." + this.id, i), r && t.on("raphael.drag.move." + this.id, r), n && t.on("raphael.drag.end." + this.id, n), t("raphael.drag.start." + this.id, s || a || this, l.clientX + f, l.clientY + c, l)
                        }
                        return this._drag = {}, be.push({
                            el: this,
                            start: l
                        }), this.mousedown(l), this
                    }, ye.onDragOver = function (e) {
                        e ? t.on("raphael.drag.over." + this.id, e) : t.unbind("raphael.drag.over." + this.id)
                    }, ye.undrag = function () {
                        for (var r = be.length; r--; ) be[r].el == this && (this.unmousedown(be[r].start), be.splice(r, 1), t.unbind("raphael.drag.*." + this.id));
                        !be.length && e.unmousemove(xe).unmouseup(ve), ge = []
                    }, M.circle = function (t, r, i) {
                        var n = e._engine.circle(this, t || 0, r || 0, i || 0);
                        return this.__set__ && this.__set__.push(n), n
                    }, M.rect = function (t, r, i, n, a) {
                        var s = e._engine.rect(this, t || 0, r || 0, i || 0, n || 0, a || 0);
                        return this.__set__ && this.__set__.push(s), s
                    }, M.ellipse = function (t, r, i, n) {
                        var a = e._engine.ellipse(this, t || 0, r || 0, i || 0, n || 0);
                        return this.__set__ && this.__set__.push(a), a
                    }, M.path = function (t) {
                        t && !e.is(t, Z) && !e.is(t[0], Q) && (t += R);
                        var r = e._engine.path(e.format[z](e, arguments), this);
                        return this.__set__ && this.__set__.push(r), r
                    }, M.image = function (t, r, i, n, a) {
                        var s = e._engine.image(this, t || "about:blank", r || 0, i || 0, n || 0, a || 0);
                        return this.__set__ && this.__set__.push(s), s
                    }, M.text = function (t, r, i) {
                        var n = e._engine.text(this, t || 0, r || 0, j(i));
                        return this.__set__ && this.__set__.push(n), n
                    }, M.set = function (t) {
                        !e.is(t, "array") && (t = Array.prototype.splice.call(arguments, 0, arguments.length));
                        var r = new ze(t);
                        return this.__set__ && this.__set__.push(r), r.paper = this, r.type = "set", r
                    }, M.setStart = function (t) {
                        this.__set__ = t || this.set()
                    }, M.setFinish = function (t) {
                        var e = this.__set__;
                        return delete this.__set__, e
                    }, M.getSize = function () {
                        var t = this.canvas.parentNode;
                        return {
                            width: t.offsetWidth,
                            height: t.offsetHeight
                        }
                    }, M.setSize = function (t, r) {
                        return e._engine.setSize.call(this, t, r)
                    }, M.setViewBox = function (t, r, i, n, a) {
                        return e._engine.setViewBox.call(this, t, r, i, n, a)
                    }, M.top = M.bottom = null, M.raphael = e;
                    var _e = function (t) {
                        var e = t.getBoundingClientRect(),
                        r = t.ownerDocument,
                        i = r.body,
                        n = r.documentElement,
                        a = n.clientTop || i.clientTop || 0,
                        s = n.clientLeft || i.clientLeft || 0,
                        o = e.top + (A.win.pageYOffset || n.scrollTop || i.scrollTop) - a,
                        l = e.left + (A.win.pageXOffset || n.scrollLeft || i.scrollLeft) - s;
                        return {
                            y: o,
                            x: l
                        }
                    };
                    M.getElementByPoint = function (t, e) {
                        var r = this,
                        i = r.canvas,
                        n = A.doc.elementFromPoint(t, e);
                        if (A.win.opera && "svg" == n.tagName) {
                            var a = _e(i),
                            s = i.createSVGRect();
                            s.x = t - a.x, s.y = e - a.y, s.width = s.height = 1;
                            var o = i.getIntersectionList(s, null);
                            o.length && (n = o[o.length - 1])
                        }
                        if (!n) return null;
                        for (; n.parentNode && n != i.parentNode && !n.raphael; ) n = n.parentNode;
                        return n == r.canvas.parentNode && (n = i), n = n && n.raphael ? r.getById(n.raphaelid) : null
                    }, M.getElementsByBBox = function (t) {
                        var r = this.set();
                        return this.forEach(function (i) {
                            e.isBBoxIntersect(i.getBBox(), t) && r.push(i)
                        }), r
                    }, M.getById = function (t) {
                        for (var e = this.bottom; e; ) {
                            if (e.id == t) return e;
                            e = e.next
                        }
                        return null
                    }, M.forEach = function (t, e) {
                        for (var r = this.bottom; r; ) {
                            if (t.call(e, r) === !1) return this;
                            r = r.next
                        }
                        return this
                    }, M.getElementsByPoint = function (t, e) {
                        var r = this.set();
                        return this.forEach(function (i) {
                            i.isPointInside(t, e) && r.push(i)
                        }), r
                    }, ye.isPointInside = function (t, r) {
                        var i = this.realPath = Et[this.type](this);
                        return this.attr("transform") && this.attr("transform").length && (i = e.transformPath(i, this.attr("transform"))), e.isPointInsidePath(i, t, r)
                    }, ye.getBBox = function (t) {
                        if (this.removed) return {};
                        var e = this._;
                        return t ? (!e.dirty && e.bboxwt || (this.realPath = Et[this.type](this), e.bboxwt = Ot(this.realPath), e.bboxwt.toString = v, e.dirty = 0), e.bboxwt) : ((e.dirty || e.dirtyT || !e.bbox) && (!e.dirty && this.realPath || (e.bboxwt = 0, this.realPath = Et[this.type](this)), e.bbox = Ot(Nt(this.realPath, this.matrix)), e.bbox.toString = v, e.dirty = e.dirtyT = 0), e.bbox)
                    }, ye.clone = function () {
                        if (this.removed) return null;
                        var t = this.paper[this.type]().attr(this.attr());
                        return this.__set__ && this.__set__.push(t), t
                    }, ye.glow = function (t) {
                        if ("text" == this.type) return null;
                        t = t || {};
                        var e = {
                            width: (t.width || 10) + (+this.attr("stroke-width") || 1),
                            fill: t.fill || !1,
                            opacity: null == t.opacity ? .5 : t.opacity,
                            offsetx: t.offsetx || 0,
                            offsety: t.offsety || 0,
                            color: t.color || "#000"
                        }, r = e.width / 2,
                        i = this.paper,
                        n = i.set(),
                        a = this.realPath || Et[this.type](this);
                        a = this.matrix ? Nt(a, this.matrix) : a;
                        for (var s = 1; r + 1 > s; s++) n.push(i.path(a).attr({
                            stroke: e.color,
                            fill: e.fill ? e.color : "none",
                            "stroke-linejoin": "round",
                            "stroke-linecap": "round",
                            "stroke-width": +(e.width / r * s).toFixed(3),
                            opacity: +(e.opacity / r).toFixed(3)
                        }));
                        return n.insertBefore(this).translate(e.offsetx, e.offsety)
                    };
                    var we = {}, ke = function (t, r, i, n, a, s, o, u, c) {
                        return null == c ? l(t, r, i, n, a, s, o, u) : e.findDotsAtSegment(t, r, i, n, a, s, o, u, h(t, r, i, n, a, s, o, u, c))
                    }, Be = function (t, r) {
                        return function (i, n, a) {
                            i = Qt(i);
                            for (var s, o, l, h, u = "", c = {}, f, p = 0, d = 0, g = i.length; g > d; d++) {
                                if (l = i[d], "M" == l[0]) s = +l[1], o = +l[2];
                                else {
                                    if (h = ke(s, o, l[1], l[2], l[3], l[4], l[5], l[6]), p + h > n) {
                                        if (r && !c.start) {
                                            if (f = ke(s, o, l[1], l[2], l[3], l[4], l[5], l[6], n - p), u += ["C" + f.start.x, f.start.y, f.m.x, f.m.y, f.x, f.y], a) return u;
                                            c.start = u, u = ["M" + f.x, f.y + "C" + f.n.x, f.n.y, f.end.x, f.end.y, l[5], l[6]].join(), p += h, s = +l[5], o = +l[6];
                                            continue
                                        }
                                        if (!t && !r) return f = ke(s, o, l[1], l[2], l[3], l[4], l[5], l[6], n - p), {
                                            x: f.x,
                                            y: f.y,
                                            alpha: f.alpha
                                        }
                                    }
                                    p += h, s = +l[5], o = +l[6]
                                }
                                u += l.shift() + l
                            }
                            return c.end = u, f = t ? p : r ? c : e.findDotsAtSegment(s, o, l[0], l[1], l[2], l[3], l[4], l[5], 1), f.alpha && (f = {
                                x: f.x,
                                y: f.y,
                                alpha: f.alpha
                            }), f
                        }
                    }, Ce = Be(1),
                    Se = Be(),
                    Te = Be(0, 1);
                    e.getTotalLength = Ce, e.getPointAtLength = Se, e.getSubpath = function (t, e, r) {
                        if (this.getTotalLength(t) - r < 1e-6) return Te(t, e).end;
                        var i = Te(t, r, 1);
                        return e ? Te(i, e).end : i
                    }, ye.getTotalLength = function () {
                        var t = this.getPath();
                        if (t) return this.node.getTotalLength ? this.node.getTotalLength() : Ce(t)
                    }, ye.getPointAtLength = function (t) {
                        var e = this.getPath();
                        if (e) return Se(e, t)
                    }, ye.getPath = function () {
                        var t, r = e._getPath[this.type];
                        if ("text" != this.type && "set" != this.type) return r && (t = r(this)), t
                    }, ye.getSubpath = function (t, r) {
                        var i = this.getPath();
                        if (i) return e.getSubpath(i, t, r)
                    };
                    var Ae = e.easing_formulas = {
                        linear: function (t) {
                            return t
                        },
                        "<": function (t) {
                            return X(t, 1.7)
                        },
                        ">": function (t) {
                            return X(t, .48)
                        },
                        "<>": function (t) {
                            var e = .48 - t / 1.04,
                            r = Y.sqrt(.1734 + e * e),
                            i = r - e,
                            n = X(H(i), 1 / 3) * (0 > i ? -1 : 1),
                            a = -r - e,
                            s = X(H(a), 1 / 3) * (0 > a ? -1 : 1),
                            o = n + s + .5;
                            return 3 * (1 - o) * o * o + o * o * o
                        },
                        backIn: function (t) {
                            var e = 1.70158;
                            return t * t * ((e + 1) * t - e)
                        },
                        backOut: function (t) {
                            t -= 1;
                            var e = 1.70158;
                            return t * t * ((e + 1) * t + e) + 1
                        },
                        elastic: function (t) {
                            return t == !!t ? t : X(2, -10 * t) * Y.sin((t - .075) * (2 * U) / .3) + 1
                        },
                        bounce: function (t) {
                            var e = 7.5625,
                            r = 2.75,
                            i;
                            return 1 / r > t ? i = e * t * t : 2 / r > t ? (t -= 1.5 / r, i = e * t * t + .75) : 2.5 / r > t ? (t -= 2.25 / r, i = e * t * t + .9375) : (t -= 2.625 / r, i = e * t * t + .984375), i
                        }
                    };
                    Ae.easeIn = Ae["ease-in"] = Ae["<"], Ae.easeOut = Ae["ease-out"] = Ae[">"], Ae.easeInOut = Ae["ease-in-out"] = Ae["<>"], Ae["back-in"] = Ae.backIn, Ae["back-out"] = Ae.backOut;
                    var Ee = [],
                    Ne = window.requestAnimationFrame || window.webkitRequestAnimationFrame || window.mozRequestAnimationFrame || window.oRequestAnimationFrame || window.msRequestAnimationFrame || function (t) {
                        setTimeout(t, 16)
                    }, Me = function () {
                        for (var r = +new Date, i = 0; i < Ee.length; i++) {
                            var n = Ee[i];
                            if (!n.el.removed && !n.paused) {
                                var a = r - n.start,
                                    s = n.ms,
                                    o = n.easing,
                                    l = n.from,
                                    h = n.diff,
                                    u = n.to,
                                    c = n.t,
                                    f = n.el,
                                    p = {}, d, g = {}, x;
                                if (n.initstatus ? (a = (n.initstatus * n.anim.top - n.prev) / (n.percent - n.prev) * s, n.status = n.initstatus, delete n.initstatus, n.stop && Ee.splice(i--, 1)) : n.status = (n.prev + (n.percent - n.prev) * (a / s)) / n.anim.top, !(0 > a))
                                    if (s > a) {
                                        var v = o(a / s);
                                        for (var y in l)
                                            if (l[T](y)) {
                                                switch (pt[y]) {
                                                    case $:
                                                        d = +l[y] + v * s * h[y];
                                                        break;
                                                    case "colour":
                                                        d = "rgb(" + [Le(ot(l[y].r + v * s * h[y].r)), Le(ot(l[y].g + v * s * h[y].g)), Le(ot(l[y].b + v * s * h[y].b))].join(",") + ")";
                                                        break;
                                                    case "path":
                                                        d = [];
                                                        for (var m = 0, _ = l[y].length; _ > m; m++) {
                                                            d[m] = [l[y][m][0]];
                                                            for (var w = 1, k = l[y][m].length; k > w; w++) d[m][w] = +l[y][m][w] + v * s * h[y][m][w];
                                                            d[m] = d[m].join(I)
                                                        }
                                                        d = d.join(I);
                                                        break;
                                                    case "transform":
                                                        if (h[y].real)
                                                            for (d = [], m = 0, _ = l[y].length; _ > m; m++)
                                                                for (d[m] = [l[y][m][0]], w = 1, k = l[y][m].length; k > w; w++) d[m][w] = l[y][m][w] + v * s * h[y][m][w];
                                                        else {
                                                            var B = function (t) {
                                                                return +l[y][t] + v * s * h[y][t]
                                                            };
                                                            d = [
                                                                ["m", B(0), B(1), B(2), B(3), B(4), B(5)]
                                                            ]
                                                        }
                                                        break;
                                                    case "csv":
                                                        if ("clip-rect" == y)
                                                            for (d = [], m = 4; m--; ) d[m] = +l[y][m] + v * s * h[y][m];
                                                        break;
                                                    default:
                                                        var C = [][P](l[y]);
                                                        for (d = [], m = f.paper.customAttributes[y].length; m--; ) d[m] = +C[m] + v * s * h[y][m]
                                                }
                                                p[y] = d
                                            }
                                        f.attr(p),
                                        function (e, r, i) {
                                            setTimeout(function () {
                                                t("raphael.anim.frame." + e, r, i)
                                            })
                                        } (f.id, f, n.anim)
                                    } else {
                                        if (function (r, i, n) {
                                            setTimeout(function () {
                                                t("raphael.anim.frame." + i.id, i, n), t("raphael.anim.finish." + i.id, i, n), e.is(r, "function") && r.call(i)
                                            })
                                        } (n.callback, f, n.anim), f.attr(u), Ee.splice(i--, 1), n.repeat > 1 && !n.next) {
                                            for (x in u) u[T](x) && (g[x] = n.totalOrigin[x]);
                                            n.el.attr(g), b(n.anim, n.el, n.anim.percents[0], null, n.totalOrigin, n.repeat - 1)
                                        }
                                        n.next && !n.stop && b(n.anim, n.el, n.next, null, n.totalOrigin, n.repeat)
                                    }
                            }
                        }
                        Ee.length && Ne(Me)
                    }, Le = function (t) {
                        return t > 255 ? 255 : 0 > t ? 0 : t
                    };
                    ye.animateWith = function (t, r, i, n, a, s) {
                        var o = this;
                        if (o.removed) return s && s.call(o), o;
                        var l = i instanceof m ? i : e.animation(i, n, a, s),
                        h, u;
                        b(l, o, l.percents[0], null, o.attr());
                        for (var c = 0, f = Ee.length; f > c; c++)
                            if (Ee[c].anim == r && Ee[c].el == t) {
                                Ee[f - 1].start = Ee[c].start;
                                break
                            }
                        return o
                    }, ye.onAnimation = function (e) {
                        return e ? t.on("raphael.anim.frame." + this.id, e) : t.unbind("raphael.anim.frame." + this.id), this
                    }, m.prototype.delay = function (t) {
                        var e = new m(this.anim, this.ms);
                        return e.times = this.times, e.del = +t || 0, e
                    }, m.prototype.repeat = function (t) {
                        var e = new m(this.anim, this.ms);
                        return e.del = this.del, e.times = Y.floor(W(t, 0)) || 1, e
                    }, e.animation = function (t, r, i, n) {
                        if (t instanceof m) return t;
                        !e.is(i, "function") && i || (n = n || i || null, i = null), t = Object(t), r = +r || 0;
                        var a = {}, s, o;
                        for (o in t) t[T](o) && ht(o) != o && ht(o) + "%" != o && (s = !0, a[o] = t[o]);
                        if (s) return i && (a.easing = i), n && (a.callback = n), new m({
                            100: a
                        }, r);
                        if (n) {
                            var l = 0;
                            for (var h in t) {
                                var u = ut(h);
                                t[T](h) && u > l && (l = u)
                            }
                            l += "%", !t[l].callback && (t[l].callback = n)
                        }
                        return new m(t, r)
                    }, ye.animate = function (t, r, i, n) {
                        var a = this;
                        if (a.removed) return n && n.call(a), a;
                        var s = t instanceof m ? t : e.animation(t, r, i, n);
                        return b(s, a, s.percents[0], null, a.attr()), a
                    }, ye.setTime = function (t, e) {
                        return t && null != e && this.status(t, G(e, t.ms) / t.ms), this
                    }, ye.status = function (t, e) {
                        var r = [],
                        i = 0,
                        n, a;
                        if (null != e) return b(t, this, -1, G(e, 1)), this;
                        for (n = Ee.length; n > i; i++)
                            if (a = Ee[i], a.el.id == this.id && (!t || a.anim == t)) {
                                if (t) return a.status;
                                r.push({
                                    anim: a.anim,
                                    status: a.status
                                })
                            }
                        return t ? 0 : r
                    }, ye.pause = function (e) {
                        for (var r = 0; r < Ee.length; r++) Ee[r].el.id != this.id || e && Ee[r].anim != e || t("raphael.anim.pause." + this.id, this, Ee[r].anim) !== !1 && (Ee[r].paused = !0);
                        return this
                    }, ye.resume = function (e) {
                        for (var r = 0; r < Ee.length; r++)
                            if (Ee[r].el.id == this.id && (!e || Ee[r].anim == e)) {
                                var i = Ee[r];
                                t("raphael.anim.resume." + this.id, this, i.anim) !== !1 && (delete i.paused, this.status(i.anim, i.status))
                            }
                        return this
                    }, ye.stop = function (e) {
                        for (var r = 0; r < Ee.length; r++) Ee[r].el.id != this.id || e && Ee[r].anim != e || t("raphael.anim.stop." + this.id, this, Ee[r].anim) !== !1 && Ee.splice(r--, 1);
                        return this
                    }, t.on("raphael.remove", _), t.on("raphael.clear", _), ye.toString = function () {
                        return "Raphaël’s object"
                    };
                    var ze = function (t) {
                        if (this.items = [], this.length = 0, this.type = "set", t)
                            for (var e = 0, r = t.length; r > e; e++) !t[e] || t[e].constructor != ye.constructor && t[e].constructor != ze || (this[this.items.length] = this.items[this.items.length] = t[e], this.length++)
                    }, Pe = ze.prototype;
                    Pe.push = function () {
                        for (var t, e, r = 0, i = arguments.length; i > r; r++) t = arguments[r], !t || t.constructor != ye.constructor && t.constructor != ze || (e = this.items.length, this[e] = this.items[e] = t, this.length++);
                        return this
                    }, Pe.pop = function () {
                        return this.length && delete this[this.length--], this.items.pop()
                    }, Pe.forEach = function (t, e) {
                        for (var r = 0, i = this.items.length; i > r; r++)
                            if (t.call(e, this.items[r], r) === !1) return this;
                        return this
                    };
                    for (var Fe in ye) ye[T](Fe) && (Pe[Fe] = function (t) {
                        return function () {
                            var e = arguments;
                            return this.forEach(function (r) {
                                r[t][z](r, e)
                            })
                        }
                    } (Fe));
                    return Pe.attr = function (t, r) {
                        if (t && e.is(t, Q) && e.is(t[0], "object"))
                            for (var i = 0, n = t.length; n > i; i++) this.items[i].attr(t[i]);
                        else
                            for (var a = 0, s = this.items.length; s > a; a++) this.items[a].attr(t, r);
                        return this
                    }, Pe.clear = function () {
                        for (; this.length; ) this.pop()
                    }, Pe.splice = function (t, e, r) {
                        t = 0 > t ? W(this.length + t, 0) : t, e = W(0, G(this.length - t, e));
                        var i = [],
                        n = [],
                        a = [],
                        s;
                        for (s = 2; s < arguments.length; s++) a.push(arguments[s]);
                        for (s = 0; e > s; s++) n.push(this[t + s]);
                        for (; s < this.length - t; s++) i.push(this[t + s]);
                        var o = a.length;
                        for (s = 0; s < o + i.length; s++) this.items[t + s] = this[t + s] = o > s ? a[s] : i[s - o];
                        for (s = this.items.length = this.length -= e - o; this[s]; ) delete this[s++];
                        return new ze(n)
                    }, Pe.exclude = function (t) {
                        for (var e = 0, r = this.length; r > e; e++)
                            if (this[e] == t) return this.splice(e, 1), !0
                        }, Pe.animate = function (t, r, i, n) {
                            (e.is(i, "function") || !i) && (n = i || null);
                            var a = this.items.length,
                        s = a,
                        o, l = this,
                        h;
                            if (!a) return this;
                            n && (h = function () {
                                ! --a && n.call(l)
                            }), i = e.is(i, Z) ? i : h;
                            var u = e.animation(t, r, i, h);
                            for (o = this.items[--s].animate(u); s--; ) this.items[s] && !this.items[s].removed && this.items[s].animateWith(o, u, u), this.items[s] && !this.items[s].removed || a--;
                            return this
                        }, Pe.insertAfter = function (t) {
                            for (var e = this.items.length; e--; ) this.items[e].insertAfter(t);
                            return this
                        }, Pe.getBBox = function () {
                            for (var t = [], e = [], r = [], i = [], n = this.items.length; n--; )
                                if (!this.items[n].removed) {
                                    var a = this.items[n].getBBox();
                                    t.push(a.x), e.push(a.y), r.push(a.x + a.width), i.push(a.y + a.height)
                                }
                            return t = G[z](0, t), e = G[z](0, e), r = W[z](0, r), i = W[z](0, i), {
                                x: t,
                                y: e,
                                x2: r,
                                y2: i,
                                width: r - t,
                                height: i - e
                            }
                        }, Pe.clone = function (t) {
                            t = this.paper.set();
                            for (var e = 0, r = this.items.length; r > e; e++) t.push(this.items[e].clone());
                            return t
                        }, Pe.toString = function () {
                            return "Raphaël‘s set"
                        }, Pe.glow = function (t) {
                            var e = this.paper.set();
                            return this.forEach(function (r, i) {
                                var n = r.glow(t);
                                null != n && n.forEach(function (t, r) {
                                    e.push(t)
                                })
                            }), e
                        }, Pe.isPointInside = function (t, e) {
                            var r = !1;
                            return this.forEach(function (i) {
                                return i.isPointInside(t, e) ? (r = !0, !1) : void 0
                            }), r
                        }, e.registerFont = function (t) {
                            if (!t.face) return t;
                            this.fonts = this.fonts || {};
                            var e = {
                                w: t.w,
                                face: {},
                                glyphs: {}
                            }, r = t.face["font-family"];
                            for (var i in t.face) t.face[T](i) && (e.face[i] = t.face[i]);
                            if (this.fonts[r] ? this.fonts[r].push(e) : this.fonts[r] = [e], !t.svg) {
                                e.face["units-per-em"] = ut(t.face["units-per-em"], 10);
                                for (var n in t.glyphs)
                                    if (t.glyphs[T](n)) {
                                        var a = t.glyphs[n];
                                        if (e.glyphs[n] = {
                                            w: a.w,
                                            k: {},
                                            d: a.d && "M" + a.d.replace(/[mlcxtrv]/g, function (t) {
                                                return {
                                                    l: "L",
                                                    c: "C",
                                                    x: "z",
                                                    t: "m",
                                                    r: "l",
                                                    v: "c"
                                                }[t] || "M"
                                            }) + "z"
                                        }, a.k)
                                            for (var s in a.k) a[T](s) && (e.glyphs[n].k[s] = a.k[s])
                                        }
                                }
                                return t
                            }, M.getFont = function (t, r, i, n) {
                                if (n = n || "normal", i = i || "normal", r = +r || {
                                    normal: 400,
                                    bold: 700,
                                    lighter: 300,
                                    bolder: 800
                                }[r] || 400, e.fonts) {
                                    var a = e.fonts[t];
                                    if (!a) {
                                        var s = new RegExp("(^|\\s)" + t.replace(/[^\w\d\s+!~.:_-]/g, R) + "(\\s|$)", "i");
                                        for (var o in e.fonts)
                                            if (e.fonts[T](o) && s.test(o)) {
                                                a = e.fonts[o];
                                                break
                                            }
                                    }
                                    var l;
                                    if (a)
                                        for (var h = 0, u = a.length; u > h && (l = a[h], l.face["font-weight"] != r || l.face["font-style"] != i && l.face["font-style"] || l.face["font-stretch"] != n); h++);
                                    return l
                                }
                            }, M.print = function (t, r, i, n, a, s, o, l) {
                                s = s || "middle", o = W(G(o || 0, 1), -1), l = W(G(l || 1, 3), 1);
                                var h = j(i)[q](R),
                        u = 0,
                        c = 0,
                        f = R,
                        p;
                                if (e.is(n, "string") && (n = this.getFont(n)), n) {
                                    p = (a || 16) / n.face["units-per-em"];
                                    for (var d = n.face.bbox[q](k), g = +d[0], x = d[3] - d[1], v = 0, y = +d[1] + ("baseline" == s ? x + +n.face.descent : x / 2), m = 0, b = h.length; b > m; m++) {
                                        if ("\n" == h[m]) u = 0, w = 0, c = 0, v += x * l;
                                        else {
                                            var _ = c && n.glyphs[h[m - 1]] || {}, w = n.glyphs[h[m]];
                                            u += c ? (_.w || n.w) + (_.k && _.k[h[m]] || 0) + n.w * o : 0, c = 1
                                        }
                                        w && w.d && (f += e.transformPath(w.d, ["t", u * p, v * p, "s", p, p, g, y, "t", (t - g) / p, (r - y) / p]))
                                    }
                                }
                                return this.path(f).attr({
                                    fill: "#000",
                                    stroke: "none"
                                })
                            }, M.add = function (t) {
                                if (e.is(t, "array"))
                                    for (var r = this.set(), i = 0, n = t.length, a; n > i; i++) a = t[i] || {}, B[T](a.type) && r.push(this[a.type]().attr(a));
                                return r
                            }, e.format = function (t, r) {
                                var i = e.is(r, Q) ? [0][P](r) : arguments;
                                return t && e.is(t, Z) && i.length - 1 && (t = t.replace(C, function (t, e) {
                                    return null == i[++e] ? R : i[e]
                                })), t || R
                            }, e.fullfill = function () {
                                var t = /\{([^\}]+)\}/g,
                        e = /(?:(?:^|\.)(.+?)(?=\[|\.|$|\()|\[('|")(.+?)\2\])(\(\))?/g,
                        r = function (t, r, i) {
                            var n = i;
                            return r.replace(e, function (t, e, r, i, a) {
                                e = e || i, n && (e in n && (n = n[e]), "function" == typeof n && a && (n = n()))
                            }), n = (null == n || n == i ? t : n) + ""
                        };
                                return function (e, i) {
                                    return String(e).replace(t, function (t, e) {
                                        return r(t, e, i)
                                    })
                                }
                            } (), e.ninja = function () {
                                if (E.was) A.win.Raphael = E.is;
                                else {
                                    window.Raphael = void 0;
                                    try {
                                        delete window.Raphael
                                    } catch (t) { }
                                }
                                return e
                            }, e.st = Pe, t.on("raphael.DOMload", function () {
                                w = !0
                            }),
                function (t, r, i) {
                    function n() {
                        /in/.test(t.readyState) ? setTimeout(n, 9) : e.eve("raphael.DOMload")
                    }
                    null == t.readyState && t.addEventListener && (t.addEventListener(r, i = function () {
                        t.removeEventListener(r, i, !1), t.readyState = "complete"
                    }, !1), t.readyState = "loading"), n()
                } (document, "DOMContentLoaded"), e
                        } .apply(e, i), !(void 0 !== n && (t.exports = n))
                    },
        function (t, e, r) {
            var i, n;
            !function (r) {
                var a = "0.4.2",
                    s = "hasOwnProperty",
                    o = /[\.\/]/,
                    l = "*",
                    h = function () { }, u = function (t, e) {
                        return t - e
                    }, c, f, p = {
                        n: {}
                    }, d = function (t, e) {
                        t = String(t);
                        var r = p,
                            i = f,
                            n = Array.prototype.slice.call(arguments, 2),
                            a = d.listeners(t),
                            s = 0,
                            o = !1,
                            l, h = [],
                            g = {}, x = [],
                            v = c,
                            y = [];
                        c = t, f = 0;
                        for (var m = 0, b = a.length; b > m; m++) "zIndex" in a[m] && (h.push(a[m].zIndex), a[m].zIndex < 0 && (g[a[m].zIndex] = a[m]));
                        for (h.sort(u); h[s] < 0; )
                            if (l = g[h[s++]], x.push(l.apply(e, n)), f) return f = i, x;
                        for (m = 0; b > m; m++)
                            if (l = a[m], "zIndex" in l)
                                if (l.zIndex == h[s]) {
                                    if (x.push(l.apply(e, n)), f) break;
                                    do
                                        if (s++, l = g[h[s]], l && x.push(l.apply(e, n)), f) break; while (l)
                                } else g[l.zIndex] = l;
                            else if (x.push(l.apply(e, n)), f) break;
                        return f = i, c = v, x.length ? x : null
                    };
                d._events = p, d.listeners = function (t) {
                    var e = t.split(o),
                        r = p,
                        i, n, a, s, h, u, c, f, d = [r],
                        g = [];
                    for (s = 0, h = e.length; h > s; s++) {
                        for (f = [], u = 0, c = d.length; c > u; u++)
                            for (r = d[u].n, n = [r[e[s]], r[l]], a = 2; a--; ) i = n[a], i && (f.push(i), g = g.concat(i.f || []));
                        d = f
                    }
                    return g
                }, d.on = function (t, e) {
                    if (t = String(t), "function" != typeof e) return function () { };
                    for (var r = t.split(o), i = p, n = 0, a = r.length; a > n; n++) i = i.n, i = i.hasOwnProperty(r[n]) && i[r[n]] || (i[r[n]] = {
                        n: {}
                    });
                    for (i.f = i.f || [], n = 0, a = i.f.length; a > n; n++)
                        if (i.f[n] == e) return h;
                    return i.f.push(e),
                    function (t) {
+t == +t && (e.zIndex = +t)
                    }
                }, d.f = function (t) {
                    var e = [].slice.call(arguments, 1);
                    return function () {
                        d.apply(null, [t, null].concat(e).concat([].slice.call(arguments, 0)))
                    }
                }, d.stop = function () {
                    f = 1
                }, d.nt = function (t) {
                    return t ? new RegExp("(?:\\.|\\/|^)" + t + "(?:\\.|\\/|$)").test(c) : c
                }, d.nts = function () {
                    return c.split(o)
                }, d.off = d.unbind = function (t, e) {
                    if (!t) return void (d._events = p = {
                        n: {}
                    });
                    var r = t.split(o),
                        i, n, a, h, u, c, f, g = [p];
                    for (h = 0, u = r.length; u > h; h++)
                        for (c = 0; c < g.length; c += a.length - 2) {
                            if (a = [c, 1], i = g[c].n, r[h] != l) i[r[h]] && a.push(i[r[h]]);
                            else
                                for (n in i) i[s](n) && a.push(i[n]);
                            g.splice.apply(g, a)
                        }
                    for (h = 0, u = g.length; u > h; h++)
                        for (i = g[h]; i.n; ) {
                            if (e) {
                                if (i.f) {
                                    for (c = 0, f = i.f.length; f > c; c++)
                                        if (i.f[c] == e) {
                                            i.f.splice(c, 1);
                                            break
                                        } !i.f.length && delete i.f
                                }
                                for (n in i.n)
                                    if (i.n[s](n) && i.n[n].f) {
                                        var x = i.n[n].f;
                                        for (c = 0, f = x.length; f > c; c++)
                                            if (x[c] == e) {
                                                x.splice(c, 1);
                                                break
                                            } !x.length && delete i.n[n].f
                                    }
                            } else {
                                delete i.f;
                                for (n in i.n) i.n[s](n) && i.n[n].f && delete i.n[n].f
                            }
                            i = i.n
                        }
                }, d.once = function (t, e) {
                    var r = function () {
                        return d.unbind(t, r), e.apply(this, arguments)
                    };
                    return d.on(t, r)
                }, d.version = a, d.toString = function () {
                    return "You are running Eve " + a
                }, "undefined" != typeof t && t.exports ? t.exports = d : (i = [], n = function () {
                    return d
                } .apply(e, i), !(void 0 !== n && (t.exports = n)))
            } (this)
        },
        function (t, e, r) {
            var i, n;
            i = [r(1)], n = function (t) {
                if (!t || t.svg) {
                    var e = "hasOwnProperty",
                        r = String,
                        i = parseFloat,
                        n = parseInt,
                        a = Math,
                        s = a.max,
                        o = a.abs,
                        l = a.pow,
                        h = /[, ]+/,
                        u = t.eve,
                        c = "",
                        f = " ",
                        p = "http://www.w3.org/1999/xlink",
                        d = {
                            block: "M5,0 0,2.5 5,5z",
                            classic: "M5,0 0,2.5 5,5 3.5,3 3.5,2z",
                            diamond: "M2.5,0 5,2.5 2.5,5 0,2.5z",
                            open: "M6,1 1,3.5 6,6",
                            oval: "M2.5,0A2.5,2.5,0,0,1,2.5,5 2.5,2.5,0,0,1,2.5,0z"
                        }, g = {};
                    t.toString = function () {
                        return "Your browser supports SVG.\nYou are running Raphaël " + this.version
                    };
                    var x = function (i, n) {
                        if (n) {
                            "string" == typeof i && (i = x(i));
                            for (var a in n) n[e](a) && ("xlink:" == a.substring(0, 6) ? i.setAttributeNS(p, a.substring(6), r(n[a])) : i.setAttribute(a, r(n[a])))
                        } else i = t._g.doc.createElementNS("http://www.w3.org/2000/svg", i), i.style && (i.style.webkitTapHighlightColor = "rgba(0,0,0,0)");
                        return i
                    }, v = function (e, n) {
                        var h = "linear",
                                u = e.id + n,
                                f = .5,
                                p = .5,
                                d = e.node,
                                g = e.paper,
                                v = d.style,
                                y = t._g.doc.getElementById(u);
                        if (!y) {
                            if (n = r(n).replace(t._radial_gradient, function (t, e, r) {
                                if (h = "radial", e && r) {
                                    f = i(e), p = i(r);
                                    var n = 2 * (p > .5) - 1;
                                    l(f - .5, 2) + l(p - .5, 2) > .25 && (p = a.sqrt(.25 - l(f - .5, 2)) * n + .5) && .5 != p && (p = p.toFixed(5) - 1e-5 * n)
                                }
                                return c
                            }), n = n.split(/\s*\-\s*/), "linear" == h) {
                                var b = n.shift();
                                if (b = -i(b), isNaN(b)) return null;
                                var _ = [0, 0, a.cos(t.rad(b)), a.sin(t.rad(b))],
                                        w = 1 / (s(o(_[2]), o(_[3])) || 1);
                                _[2] *= w, _[3] *= w, _[2] < 0 && (_[0] = -_[2], _[2] = 0), _[3] < 0 && (_[1] = -_[3], _[3] = 0)
                            }
                            var k = t._parseDots(n);
                            if (!k) return null;
                            if (u = u.replace(/[\(\)\s,\xb0#]/g, "_"), e.gradient && u != e.gradient.id && (g.defs.removeChild(e.gradient), delete e.gradient), !e.gradient) {
                                y = x(h + "Gradient", {
                                    id: u
                                }), e.gradient = y, x(y, "radial" == h ? {
                                    fx: f,
                                    fy: p
                                } : {
                                    x1: _[0],
                                    y1: _[1],
                                    x2: _[2],
                                    y2: _[3],
                                    gradientTransform: e.matrix.invert()
                                }), g.defs.appendChild(y);
                                for (var B = 0, C = k.length; C > B; B++) y.appendChild(x("stop", {
                                    offset: k[B].offset ? k[B].offset : B ? "100%" : "0%",
                                    "stop-color": k[B].color || "#fff",
                                    "stop-opacity": isFinite(k[B].opacity) ? k[B].opacity : 1
                                }))
                            }
                        }
                        return x(d, {
                            fill: m(u),
                            opacity: 1,
                            "fill-opacity": 1
                        }), v.fill = c, v.opacity = 1, v.fillOpacity = 1, 1
                    }, y = function () {
                        var t = document.documentMode;
                        return t && (9 === t || 10 === t)
                    }, m = function (t) {
                        if (y()) return "url('#" + t + "')";
                        var e = document.location,
                                r = e.protocol + "//" + e.host + e.pathname + e.search;
                        return "url('" + r + "#" + t + "')"
                    }, b = function (t) {
                        var e = t.getBBox(1);
                        x(t.pattern, {
                            patternTransform: t.matrix.invert() + " translate(" + e.x + "," + e.y + ")"
                        })
                    }, _ = function (i, n, a) {
                        if ("path" == i.type) {
                            for (var s = r(n).toLowerCase().split("-"), o = i.paper, l = a ? "end" : "start", h = i.node, u = i.attrs, f = u["stroke-width"], p = s.length, v = "classic", y, m, b, _, w, k = 3, B = 3, C = 5; p--; ) switch (s[p]) {
                                case "block":
                                case "classic":
                                case "oval":
                                case "diamond":
                                case "open":
                                case "none":
                                    v = s[p];
                                    break;
                                case "wide":
                                    B = 5;
                                    break;
                                case "narrow":
                                    B = 2;
                                    break;
                                case "long":
                                    k = 5;
                                    break;
                                case "short":
                                    k = 2
                            }
                            if ("open" == v ? (k += 2, B += 2, C += 2, b = 1, _ = a ? 4 : 1, w = {
                                fill: "none",
                                stroke: u.stroke
                            }) : (_ = b = k / 2, w = {
                                fill: u.stroke,
                                stroke: "none"
                            }), i._.arrows ? a ? (i._.arrows.endPath && g[i._.arrows.endPath]--, i._.arrows.endMarker && g[i._.arrows.endMarker]--) : (i._.arrows.startPath && g[i._.arrows.startPath]--, i._.arrows.startMarker && g[i._.arrows.startMarker]--) : i._.arrows = {}, "none" != v) {
                                var S = "raphael-marker-" + v,
                                        T = "raphael-marker-" + l + v + k + B + "-obj" + i.id;
                                t._g.doc.getElementById(S) ? g[S]++ : (o.defs.appendChild(x(x("path"), {
                                    "stroke-linecap": "round",
                                    d: d[v],
                                    id: S
                                })), g[S] = 1);
                                var A = t._g.doc.getElementById(T),
                                        E;
                                A ? (g[T]++, E = A.getElementsByTagName("use")[0]) : (A = x(x("marker"), {
                                    id: T,
                                    markerHeight: B,
                                    markerWidth: k,
                                    orient: "auto",
                                    refX: _,
                                    refY: B / 2
                                }), E = x(x("use"), {
                                    "xlink:href": "#" + S,
                                    transform: (a ? "rotate(180 " + k / 2 + " " + B / 2 + ") " : c) + "scale(" + k / C + "," + B / C + ")",
                                    "stroke-width": (1 / ((k / C + B / C) / 2)).toFixed(4)
                                }), A.appendChild(E), o.defs.appendChild(A), g[T] = 1), x(E, w);
                                var N = b * ("diamond" != v && "oval" != v);
                                a ? (y = i._.arrows.startdx * f || 0, m = t.getTotalLength(u.path) - N * f) : (y = N * f, m = t.getTotalLength(u.path) - (i._.arrows.enddx * f || 0)), w = {}, w["marker-" + l] = "url(#" + T + ")", (m || y) && (w.d = t.getSubpath(u.path, y, m)), x(h, w), i._.arrows[l + "Path"] = S, i._.arrows[l + "Marker"] = T, i._.arrows[l + "dx"] = N, i._.arrows[l + "Type"] = v, i._.arrows[l + "String"] = n
                            } else a ? (y = i._.arrows.startdx * f || 0, m = t.getTotalLength(u.path) - y) : (y = 0, m = t.getTotalLength(u.path) - (i._.arrows.enddx * f || 0)), i._.arrows[l + "Path"] && x(h, {
                                d: t.getSubpath(u.path, y, m)
                            }), delete i._.arrows[l + "Path"], delete i._.arrows[l + "Marker"], delete i._.arrows[l + "dx"], delete i._.arrows[l + "Type"], delete i._.arrows[l + "String"];
                            for (w in g)
                                if (g[e](w) && !g[w]) {
                                    var M = t._g.doc.getElementById(w);
                                    M && M.parentNode.removeChild(M)
                                }
                        }
                    }, w = {
                        "-": [3, 1],
                        ".": [1, 1],
                        "-.": [3, 1, 1, 1],
                        "-..": [3, 1, 1, 1, 1, 1],
                        ". ": [1, 3],
                        "- ": [4, 3],
                        "--": [8, 3],
                        "- .": [4, 3, 1, 3],
                        "--.": [8, 3, 1, 3],
                        "--..": [8, 3, 1, 3, 1, 3]
                    }, k = function (t, e, i) {
                        if (e = w[r(e).toLowerCase()]) {
                            for (var n = t.attrs["stroke-width"] || "1", a = {
                                round: n,
                                square: n,
                                butt: 0
                            }[t.attrs["stroke-linecap"] || i["stroke-linecap"]] || 0, s = [], o = e.length; o--; ) s[o] = e[o] * n + (o % 2 ? 1 : -1) * a;
                            x(t.node, {
                                "stroke-dasharray": s.join(",")
                            })
                        } else x(t.node, {
                            "stroke-dasharray": "none"
                        })
                    }, B = function (i, a) {
                        var l = i.node,
                                u = i.attrs,
                                f = l.style.visibility;
                        l.style.visibility = "hidden";
                        for (var d in a)
                            if (a[e](d)) {
                                if (!t._availableAttrs[e](d)) continue;
                                var g = a[d];
                                switch (u[d] = g, d) {
                                    case "blur":
                                        i.blur(g);
                                        break;
                                    case "title":
                                        var y = l.getElementsByTagName("title");
                                        if (y.length && (y = y[0])) y.firstChild.nodeValue = g;
                                        else {
                                            y = x("title");
                                            var m = t._g.doc.createTextNode(g);
                                            y.appendChild(m), l.appendChild(y)
                                        }
                                        break;
                                    case "href":
                                    case "target":
                                        var w = l.parentNode;
                                        if ("a" != w.tagName.toLowerCase()) {
                                            var B = x("a");
                                            w.insertBefore(B, l), B.appendChild(l), w = B
                                        }
                                        "target" == d ? w.setAttributeNS(p, "show", "blank" == g ? "new" : g) : w.setAttributeNS(p, d, g);
                                        break;
                                    case "cursor":
                                        l.style.cursor = g;
                                        break;
                                    case "transform":
                                        i.transform(g);
                                        break;
                                    case "arrow-start":
                                        _(i, g);
                                        break;
                                    case "arrow-end":
                                        _(i, g, 1);
                                        break;
                                    case "clip-rect":
                                        var C = r(g).split(h);
                                        if (4 == C.length) {
                                            i.clip && i.clip.parentNode.parentNode.removeChild(i.clip.parentNode);
                                            var T = x("clipPath"),
                                                    A = x("rect");
                                            T.id = t.createUUID(), x(A, {
                                                x: C[0],
                                                y: C[1],
                                                width: C[2],
                                                height: C[3]
                                            }), T.appendChild(A), i.paper.defs.appendChild(T), x(l, {
                                                "clip-path": "url(#" + T.id + ")"
                                            }), i.clip = A
                                        }
                                        if (!g) {
                                            var E = l.getAttribute("clip-path");
                                            if (E) {
                                                var N = t._g.doc.getElementById(E.replace(/(^url\(#|\)$)/g, c));
                                                N && N.parentNode.removeChild(N), x(l, {
                                                    "clip-path": c
                                                }), delete i.clip
                                            }
                                        }
                                        break;
                                    case "path":
                                        "path" == i.type && (x(l, {
                                            d: g ? u.path = t._pathToAbsolute(g) : "M0,0"
                                        }), i._.dirty = 1, i._.arrows && ("startString" in i._.arrows && _(i, i._.arrows.startString), "endString" in i._.arrows && _(i, i._.arrows.endString, 1)));
                                        break;
                                    case "width":
                                        if (l.setAttribute(d, g), i._.dirty = 1, !u.fx) break;
                                        d = "x", g = u.x;
                                    case "x":
                                        u.fx && (g = -u.x - (u.width || 0));
                                    case "rx":
                                        if ("rx" == d && "rect" == i.type) break;
                                    case "cx":
                                        l.setAttribute(d, g), i.pattern && b(i), i._.dirty = 1;
                                        break;
                                    case "height":
                                        if (l.setAttribute(d, g), i._.dirty = 1, !u.fy) break;
                                        d = "y", g = u.y;
                                    case "y":
                                        u.fy && (g = -u.y - (u.height || 0));
                                    case "ry":
                                        if ("ry" == d && "rect" == i.type) break;
                                    case "cy":
                                        l.setAttribute(d, g), i.pattern && b(i), i._.dirty = 1;
                                        break;
                                    case "r":
                                        "rect" == i.type ? x(l, {
                                            rx: g,
                                            ry: g
                                        }) : l.setAttribute(d, g), i._.dirty = 1;
                                        break;
                                    case "src":
                                        "image" == i.type && l.setAttributeNS(p, "href", g);
                                        break;
                                    case "stroke-width":
                                        1 == i._.sx && 1 == i._.sy || (g /= s(o(i._.sx), o(i._.sy)) || 1), l.setAttribute(d, g), u["stroke-dasharray"] && k(i, u["stroke-dasharray"], a), i._.arrows && ("startString" in i._.arrows && _(i, i._.arrows.startString), "endString" in i._.arrows && _(i, i._.arrows.endString, 1));
                                        break;
                                    case "stroke-dasharray":
                                        k(i, g, a);
                                        break;
                                    case "fill":
                                        var M = r(g).match(t._ISURL);
                                        if (M) {
                                            T = x("pattern");
                                            var L = x("image");
                                            T.id = t.createUUID(), x(T, {
                                                x: 0,
                                                y: 0,
                                                patternUnits: "userSpaceOnUse",
                                                height: 1,
                                                width: 1
                                            }), x(L, {
                                                x: 0,
                                                y: 0,
                                                "xlink:href": M[1]
                                            }), T.appendChild(L),
                                                function (e) {
                                                    t._preload(M[1], function () {
                                                        var t = this.offsetWidth,
                                                            r = this.offsetHeight;
                                                        x(e, {
                                                            width: t,
                                                            height: r
                                                        }), x(L, {
                                                            width: t,
                                                            height: r
                                                        })
                                                    })
                                                } (T), i.paper.defs.appendChild(T), x(l, {
                                                    fill: "url(#" + T.id + ")"
                                                }), i.pattern = T, i.pattern && b(i);
                                            break
                                        }
                                        var z = t.getRGB(g);
                                        if (z.error) {
                                            if (("circle" == i.type || "ellipse" == i.type || "r" != r(g).charAt()) && v(i, g)) {
                                                if ("opacity" in u || "fill-opacity" in u) {
                                                    var P = t._g.doc.getElementById(l.getAttribute("fill").replace(/^url\(#|\)$/g, c));
                                                    if (P) {
                                                        var F = P.getElementsByTagName("stop");
                                                        x(F[F.length - 1], {
                                                            "stop-opacity": ("opacity" in u ? u.opacity : 1) * ("fill-opacity" in u ? u["fill-opacity"] : 1)
                                                        })
                                                    }
                                                }
                                                u.gradient = g, u.fill = "none";
                                                break
                                            }
                                        } else delete a.gradient, delete u.gradient, !t.is(u.opacity, "undefined") && t.is(a.opacity, "undefined") && x(l, {
                                            opacity: u.opacity
                                        }), !t.is(u["fill-opacity"], "undefined") && t.is(a["fill-opacity"], "undefined") && x(l, {
                                            "fill-opacity": u["fill-opacity"]
                                        });
                                        z[e]("opacity") && x(l, {
                                            "fill-opacity": z.opacity > 1 ? z.opacity / 100 : z.opacity
                                        });
                                    case "stroke":
                                        z = t.getRGB(g), l.setAttribute(d, z.hex), "stroke" == d && z[e]("opacity") && x(l, {
                                            "stroke-opacity": z.opacity > 1 ? z.opacity / 100 : z.opacity
                                        }), "stroke" == d && i._.arrows && ("startString" in i._.arrows && _(i, i._.arrows.startString), "endString" in i._.arrows && _(i, i._.arrows.endString, 1));
                                        break;
                                    case "gradient":
                                        ("circle" == i.type || "ellipse" == i.type || "r" != r(g).charAt()) && v(i, g);
                                        break;
                                    case "opacity":
                                        u.gradient && !u[e]("stroke-opacity") && x(l, {
                                            "stroke-opacity": g > 1 ? g / 100 : g
                                        });
                                    case "fill-opacity":
                                        if (u.gradient) {
                                            P = t._g.doc.getElementById(l.getAttribute("fill").replace(/^url\(#|\)$/g, c)), P && (F = P.getElementsByTagName("stop"), x(F[F.length - 1], {
                                                "stop-opacity": g
                                            }));
                                            break
                                        }
                                    default:
                                        "font-size" == d && (g = n(g, 10) + "px");
                                        var R = d.replace(/(\-.)/g, function (t) {
                                            return t.substring(1).toUpperCase()
                                        });
                                        l.style[R] = g, i._.dirty = 1, l.setAttribute(d, g)
                                }
                            }
                        S(i, a), l.style.visibility = f
                    }, C = 1.2,
                        S = function (i, a) {
                            if ("text" == i.type && (a[e]("text") || a[e]("font") || a[e]("font-size") || a[e]("x") || a[e]("y"))) {
                                var s = i.attrs,
                                    o = i.node,
                                    l = o.firstChild ? n(t._g.doc.defaultView.getComputedStyle(o.firstChild, c).getPropertyValue("font-size"), 10) : 10;
                                if (a[e]("text")) {
                                    for (s.text = a.text; o.firstChild; ) o.removeChild(o.firstChild);
                                    for (var h = r(a.text).split("\n"), u = [], f, p = 0, d = h.length; d > p; p++) f = x("tspan"), p && x(f, {
                                        dy: l * C,
                                        x: s.x
                                    }), f.appendChild(t._g.doc.createTextNode(h[p])), o.appendChild(f), u[p] = f
                                } else
                                    for (u = o.getElementsByTagName("tspan"), p = 0, d = u.length; d > p; p++) p ? x(u[p], {
                                        dy: l * C,
                                        x: s.x
                                    }) : x(u[0], {
                                        dy: 0
                                    });
                                x(o, {
                                    x: s.x,
                                    y: s.y
                                }), i._.dirty = 1;
                                var g = i._getBBox(),
                                    v = s.y - (g.y + g.height / 2);
                                v && t.is(v, "finite") && x(u[0], {
                                    dy: v
                                })
                            }
                        }, T = function (t) {
                            return t.parentNode && "a" === t.parentNode.tagName.toLowerCase() ? t.parentNode : t
                        }, A = function (e, r) {
                            var i = 0,
                                n = 0;
                            this[0] = this.node = e, e.raphael = !0, this.id = t._oid++, e.raphaelid = this.id, this.matrix = t.matrix(), this.realPath = null, this.paper = r, this.attrs = this.attrs || {}, this._ = {
                                transform: [],
                                sx: 1,
                                sy: 1,
                                deg: 0,
                                dx: 0,
                                dy: 0,
                                dirty: 1
                            }, !r.bottom && (r.bottom = this), this.prev = r.top, r.top && (r.top.next = this), r.top = this, this.next = null
                        }, E = t.el;
                    A.prototype = E, E.constructor = A, t._engine.path = function (t, e) {
                        var r = x("path");
                        e.canvas && e.canvas.appendChild(r);
                        var i = new A(r, e);
                        return i.type = "path", B(i, {
                            fill: "none",
                            stroke: "#000",
                            path: t
                        }), i
                    }, E.rotate = function (t, e, n) {
                        if (this.removed) return this;
                        if (t = r(t).split(h), t.length - 1 && (e = i(t[1]), n = i(t[2])), t = i(t[0]), null == n && (e = n), null == e || null == n) {
                            var a = this.getBBox(1);
                            e = a.x + a.width / 2, n = a.y + a.height / 2
                        }
                        return this.transform(this._.transform.concat([
                            ["r", t, e, n]
                        ])), this
                    }, E.scale = function (t, e, n, a) {
                        if (this.removed) return this;
                        if (t = r(t).split(h), t.length - 1 && (e = i(t[1]), n = i(t[2]), a = i(t[3])), t = i(t[0]), null == e && (e = t), null == a && (n = a), null == n || null == a) var s = this.getBBox(1);
                        return n = null == n ? s.x + s.width / 2 : n, a = null == a ? s.y + s.height / 2 : a, this.transform(this._.transform.concat([
                            ["s", t, e, n, a]
                        ])), this
                    }, E.translate = function (t, e) {
                        return this.removed ? this : (t = r(t).split(h), t.length - 1 && (e = i(t[1])), t = i(t[0]) || 0, e = +e || 0, this.transform(this._.transform.concat([
                            ["t", t, e]
                        ])), this)
                    }, E.transform = function (r) {
                        var i = this._;
                        if (null == r) return i.transform;
                        if (t._extractTransform(this, r), this.clip && x(this.clip, {
                            transform: this.matrix.invert()
                        }), this.pattern && b(this), this.node && x(this.node, {
                            transform: this.matrix
                        }), 1 != i.sx || 1 != i.sy) {
                            var n = this.attrs[e]("stroke-width") ? this.attrs["stroke-width"] : 1;
                            this.attr({
                                "stroke-width": n
                            })
                        }
                        return i.transform = this.matrix.toTransformString(), this
                    }, E.hide = function () {
                        return this.removed || (this.node.style.display = "none"), this
                    }, E.show = function () {
                        return this.removed || (this.node.style.display = ""), this
                    }, E.remove = function () {
                        var e = T(this.node);
                        if (!this.removed && e.parentNode) {
                            var r = this.paper;
                            r.__set__ && r.__set__.exclude(this), u.unbind("raphael.*.*." + this.id), this.gradient && r.defs.removeChild(this.gradient), t._tear(this, r), e.parentNode.removeChild(e), this.removeData();
                            for (var i in this) this[i] = "function" == typeof this[i] ? t._removedFactory(i) : null;
                            this.removed = !0
                        }
                    }, E._getBBox = function () {
                        if ("none" == this.node.style.display) {
                            this.show();
                            var t = !0
                        }
                        var e = !1,
                            r;
                        this.paper.canvas.parentElement ? r = this.paper.canvas.parentElement.style : this.paper.canvas.parentNode && (r = this.paper.canvas.parentNode.style), r && "none" == r.display && (e = !0, r.display = "");
                        var i = {};
                        try {
                            i = this.node.getBBox()
                        } catch (n) {
                            i = {
                                x: this.node.clientLeft,
                                y: this.node.clientTop,
                                width: this.node.clientWidth,
                                height: this.node.clientHeight
                            }
                        } finally {
                            i = i || {}, e && (r.display = "none")
                        }
                        return t && this.hide(), i
                    }, E.attr = function (r, i) {
                        if (this.removed) return this;
                        if (null == r) {
                            var n = {};
                            for (var a in this.attrs) this.attrs[e](a) && (n[a] = this.attrs[a]);
                            return n.gradient && "none" == n.fill && (n.fill = n.gradient) && delete n.gradient, n.transform = this._.transform, n
                        }
                        if (null == i && t.is(r, "string")) {
                            if ("fill" == r && "none" == this.attrs.fill && this.attrs.gradient) return this.attrs.gradient;
                            if ("transform" == r) return this._.transform;
                            for (var s = r.split(h), o = {}, l = 0, c = s.length; c > l; l++) r = s[l], r in this.attrs ? o[r] = this.attrs[r] : t.is(this.paper.customAttributes[r], "function") ? o[r] = this.paper.customAttributes[r].def : o[r] = t._availableAttrs[r];
                            return c - 1 ? o : o[s[0]]
                        }
                        if (null == i && t.is(r, "array")) {
                            for (o = {}, l = 0, c = r.length; c > l; l++) o[r[l]] = this.attr(r[l]);
                            return o
                        }
                        if (null != i) {
                            var f = {};
                            f[r] = i
                        } else null != r && t.is(r, "object") && (f = r);
                        for (var p in f) u("raphael.attr." + p + "." + this.id, this, f[p]);
                        for (p in this.paper.customAttributes)
                            if (this.paper.customAttributes[e](p) && f[e](p) && t.is(this.paper.customAttributes[p], "function")) {
                                var d = this.paper.customAttributes[p].apply(this, [].concat(f[p]));
                                this.attrs[p] = f[p];
                                for (var g in d) d[e](g) && (f[g] = d[g])
                            }
                        return B(this, f), this
                    }, E.toFront = function () {
                        if (this.removed) return this;
                        var e = T(this.node);
                        e.parentNode.appendChild(e);
                        var r = this.paper;
                        return r.top != this && t._tofront(this, r), this
                    }, E.toBack = function () {
                        if (this.removed) return this;
                        var e = T(this.node),
                            r = e.parentNode;
                        r.insertBefore(e, r.firstChild), t._toback(this, this.paper);
                        var i = this.paper;
                        return this
                    }, E.insertAfter = function (e) {
                        if (this.removed || !e) return this;
                        var r = T(this.node),
                            i = T(e.node || e[e.length - 1].node);
                        return i.nextSibling ? i.parentNode.insertBefore(r, i.nextSibling) : i.parentNode.appendChild(r), t._insertafter(this, e, this.paper), this
                    }, E.insertBefore = function (e) {
                        if (this.removed || !e) return this;
                        var r = T(this.node),
                            i = T(e.node || e[0].node);
                        return i.parentNode.insertBefore(r, i), t._insertbefore(this, e, this.paper), this
                    }, E.blur = function (e) {
                        var r = this;
                        if (0 !== +e) {
                            var i = x("filter"),
                                n = x("feGaussianBlur");
                            r.attrs.blur = e, i.id = t.createUUID(), x(n, {
                                stdDeviation: +e || 1.5
                            }), i.appendChild(n), r.paper.defs.appendChild(i), r._blur = i, x(r.node, {
                                filter: "url(#" + i.id + ")"
                            })
                        } else r._blur && (r._blur.parentNode.removeChild(r._blur), delete r._blur, delete r.attrs.blur), r.node.removeAttribute("filter");
                        return r
                    }, t._engine.circle = function (t, e, r, i) {
                        var n = x("circle");
                        t.canvas && t.canvas.appendChild(n);
                        var a = new A(n, t);
                        return a.attrs = {
                            cx: e,
                            cy: r,
                            r: i,
                            fill: "none",
                            stroke: "#000"
                        }, a.type = "circle", x(n, a.attrs), a
                    }, t._engine.rect = function (t, e, r, i, n, a) {
                        var s = x("rect");
                        t.canvas && t.canvas.appendChild(s);
                        var o = new A(s, t);
                        return o.attrs = {
                            x: e,
                            y: r,
                            width: i,
                            height: n,
                            rx: a || 0,
                            ry: a || 0,
                            fill: "none",
                            stroke: "#000"
                        }, o.type = "rect", x(s, o.attrs), o
                    }, t._engine.ellipse = function (t, e, r, i, n) {
                        var a = x("ellipse");
                        t.canvas && t.canvas.appendChild(a);
                        var s = new A(a, t);
                        return s.attrs = {
                            cx: e,
                            cy: r,
                            rx: i,
                            ry: n,
                            fill: "none",
                            stroke: "#000"
                        }, s.type = "ellipse", x(a, s.attrs), s
                    }, t._engine.image = function (t, e, r, i, n, a) {
                        var s = x("image");
                        x(s, {
                            x: r,
                            y: i,
                            width: n,
                            height: a,
                            preserveAspectRatio: "none"
                        }), s.setAttributeNS(p, "href", e), t.canvas && t.canvas.appendChild(s);
                        var o = new A(s, t);
                        return o.attrs = {
                            x: r,
                            y: i,
                            width: n,
                            height: a,
                            src: e
                        }, o.type = "image", o
                    }, t._engine.text = function (e, r, i, n) {
                        var a = x("text");
                        e.canvas && e.canvas.appendChild(a);
                        var s = new A(a, e);
                        return s.attrs = {
                            x: r,
                            y: i,
                            "text-anchor": "middle",
                            text: n,
                            "font-family": t._availableAttrs["font-family"],
                            "font-size": t._availableAttrs["font-size"],
                            stroke: "none",
                            fill: "#000"
                        }, s.type = "text", B(s, s.attrs), s
                    }, t._engine.setSize = function (t, e) {
                        return this.width = t || this.width, this.height = e || this.height, this.canvas.setAttribute("width", this.width), this.canvas.setAttribute("height", this.height), this._viewBox && this.setViewBox.apply(this, this._viewBox), this
                    }, t._engine.create = function () {
                        var e = t._getContainer.apply(0, arguments),
                            r = e && e.container,
                            i = e.x,
                            n = e.y,
                            a = e.width,
                            s = e.height;
                        if (!r) throw new Error("SVG container not found.");
                        var o = x("svg"),
                            l = "overflow:hidden;",
                            h;
                        return i = i || 0, n = n || 0, a = a || 512, s = s || 342, x(o, {
                            height: s,
                            version: 1.1,
                            width: a,
                            xmlns: "http://www.w3.org/2000/svg",
                            "xmlns:xlink": "http://www.w3.org/1999/xlink"
                        }), 1 == r ? (o.style.cssText = l + "position:absolute;left:" + i + "px;top:" + n + "px", t._g.doc.body.appendChild(o), h = 1) : (o.style.cssText = l + "position:relative", r.firstChild ? r.insertBefore(o, r.firstChild) : r.appendChild(o)), r = new t._Paper, r.width = a, r.height = s, r.canvas = o, r.clear(), r._left = r._top = 0, h && (r.renderfix = function () { }), r.renderfix(), r
                    }, t._engine.setViewBox = function (t, e, r, i, n) {
                        u("raphael.setViewBox", this, this._viewBox, [t, e, r, i, n]);
                        var a = this.getSize(),
                            o = s(r / a.width, i / a.height),
                            l = this.top,
                            h = n ? "xMidYMid meet" : "xMinYMin",
                            c, p;
                        for (null == t ? (this._vbSize && (o = 1), delete this._vbSize, c = "0 0 " + this.width + f + this.height) : (this._vbSize = o, c = t + f + e + f + r + f + i), x(this.canvas, {
                            viewBox: c,
                            preserveAspectRatio: h
                        }); o && l; ) p = "stroke-width" in l.attrs ? l.attrs["stroke-width"] : 1, l.attr({
                            "stroke-width": p
                        }), l._.dirty = 1, l._.dirtyT = 1, l = l.prev;
                        return this._viewBox = [t, e, r, i, !!n], this
                    }, t.prototype.renderfix = function () {
                        var t = this.canvas,
                            e = t.style,
                            r;
                        try {
                            r = t.getScreenCTM() || t.createSVGMatrix()
                        } catch (i) {
                            r = t.createSVGMatrix()
                        }
                        var n = -r.e % 1,
                            a = -r.f % 1;
                        (n || a) && (n && (this._left = (this._left + n) % 1, e.left = this._left + "px"), a && (this._top = (this._top + a) % 1, e.top = this._top + "px"))
                    }, t.prototype.clear = function () {
                        t.eve("raphael.clear", this);
                        for (var e = this.canvas; e.firstChild; ) e.removeChild(e.firstChild);
                        this.bottom = this.top = null, (this.desc = x("desc")).appendChild(t._g.doc.createTextNode("Created with Raphaël " + t.version)), e.appendChild(this.desc), e.appendChild(this.defs = x("defs"))
                    }, t.prototype.remove = function () {
                        u("raphael.remove", this), this.canvas.parentNode && this.canvas.parentNode.removeChild(this.canvas);
                        for (var e in this) this[e] = "function" == typeof this[e] ? t._removedFactory(e) : null
                    };
                    var N = t.st;
                    for (var M in E) E[e](M) && !N[e](M) && (N[M] = function (t) {
                        return function () {
                            var e = arguments;
                            return this.forEach(function (r) {
                                r[t].apply(r, e)
                            })
                        }
                    } (M))
                }
            } .apply(e, i), !(void 0 !== n && (t.exports = n))
        },
        function (t, e, r) {
            var i, n;
            i = [r(1)], n = function (t) {
                if (!t || t.vml) {
                    var e = "hasOwnProperty",
                        r = String,
                        i = parseFloat,
                        n = Math,
                        a = n.round,
                        s = n.max,
                        o = n.min,
                        l = n.abs,
                        h = "fill",
                        u = /[, ]+/,
                        c = t.eve,
                        f = " progid:DXImageTransform.Microsoft",
                        p = " ",
                        d = "",
                        g = {
                            M: "m",
                            L: "l",
                            C: "c",
                            Z: "x",
                            m: "t",
                            l: "r",
                            c: "v",
                            z: "x"
                        }, x = /([clmz]),?([^clmz]*)/gi,
                        v = / progid:\S+Blur\([^\)]+\)/g,
                        y = /-?[^,\s-]+/g,
                        m = "position:absolute;left:0;top:0;width:1px;height:1px;behavior:url(#default#VML)",
                        b = 21600,
                        _ = {
                            path: 1,
                            rect: 1,
                            image: 1
                        }, w = {
                            circle: 1,
                            ellipse: 1
                        }, k = function (e) {
                            var i = /[ahqstv]/gi,
                                n = t._pathToAbsolute;
                            if (r(e).match(i) && (n = t._path2curve), i = /[clmz]/g, n == t._pathToAbsolute && !r(e).match(i)) {
                                var s = r(e).replace(x, function (t, e, r) {
                                    var i = [],
                                        n = "m" == e.toLowerCase(),
                                        s = g[e];
                                    return r.replace(y, function (t) {
                                        n && 2 == i.length && (s += i + g["m" == e ? "l" : "L"], i = []), i.push(a(t * b))
                                    }), s + i
                                });
                                return s
                            }
                            var o = n(e),
                                l, h;
                            s = [];
                            for (var u = 0, c = o.length; c > u; u++) {
                                l = o[u], h = o[u][0].toLowerCase(), "z" == h && (h = "x");
                                for (var f = 1, v = l.length; v > f; f++) h += a(l[f] * b) + (f != v - 1 ? "," : d);
                                s.push(h)
                            }
                            return s.join(p)
                        }, B = function (e, r, i) {
                            var n = t.matrix();
                            return n.rotate(-e, .5, .5), {
                                dx: n.x(r, i),
                                dy: n.y(r, i)
                            }
                        }, C = function (t, e, r, i, n, a) {
                            var s = t._,
                                o = t.matrix,
                                u = s.fillpos,
                                c = t.node,
                                f = c.style,
                                d = 1,
                                g = "",
                                x, v = b / e,
                                y = b / r;
                            if (f.visibility = "hidden", e && r) {
                                if (c.coordsize = l(v) + p + l(y), f.rotation = a * (0 > e * r ? -1 : 1), a) {
                                    var m = B(a, i, n);
                                    i = m.dx, n = m.dy
                                }
                                if (0 > e && (g += "x"), 0 > r && (g += " y") && (d = -1), f.flip = g, c.coordorigin = i * -v + p + n * -y, u || s.fillsize) {
                                    var _ = c.getElementsByTagName(h);
                                    _ = _ && _[0], c.removeChild(_), u && (m = B(a, o.x(u[0], u[1]), o.y(u[0], u[1])), _.position = m.dx * d + p + m.dy * d), s.fillsize && (_.size = s.fillsize[0] * l(e) + p + s.fillsize[1] * l(r)), c.appendChild(_)
                                }
                                f.visibility = "visible"
                            }
                        };
                    t.toString = function () {
                        return "Your browser doesn’t support SVG. Falling down to VML.\nYou are running Raphaël " + this.version
                    };
                    var S = function (t, e, i) {
                        for (var n = r(e).toLowerCase().split("-"), a = i ? "end" : "start", s = n.length, o = "classic", l = "medium", h = "medium"; s--; ) switch (n[s]) {
                            case "block":
                            case "classic":
                            case "oval":
                            case "diamond":
                            case "open":
                            case "none":
                                o = n[s];
                                break;
                            case "wide":
                            case "narrow":
                                h = n[s];
                                break;
                            case "long":
                            case "short":
                                l = n[s]
                        }
                        var u = t.node.getElementsByTagName("stroke")[0];
                        u[a + "arrow"] = o, u[a + "arrowlength"] = l, u[a + "arrowwidth"] = h
                    }, T = function (n, l) {
                        n.attrs = n.attrs || {};
                        var c = n.node,
                                f = n.attrs,
                                g = c.style,
                                x, v = _[n.type] && (l.x != f.x || l.y != f.y || l.width != f.width || l.height != f.height || l.cx != f.cx || l.cy != f.cy || l.rx != f.rx || l.ry != f.ry || l.r != f.r),
                                y = w[n.type] && (f.cx != l.cx || f.cy != l.cy || f.r != l.r || f.rx != l.rx || f.ry != l.ry),
                                m = n;
                        for (var B in l) l[e](B) && (f[B] = l[B]);
                        if (v && (f.path = t._getPath[n.type](n), n._.dirty = 1), l.href && (c.href = l.href), l.title && (c.title = l.title), l.target && (c.target = l.target), l.cursor && (g.cursor = l.cursor), "blur" in l && n.blur(l.blur), (l.path && "path" == n.type || v) && (c.path = k(~r(f.path).toLowerCase().indexOf("r") ? t._pathToAbsolute(f.path) : f.path), n._.dirty = 1, "image" == n.type && (n._.fillpos = [f.x, f.y], n._.fillsize = [f.width, f.height], C(n, 1, 1, 0, 0, 0))), "transform" in l && n.transform(l.transform), y) {
                            var T = +f.cx,
                                    E = +f.cy,
                                    N = +f.rx || +f.r || 0,
                                    L = +f.ry || +f.r || 0;
                            c.path = t.format("ar{0},{1},{2},{3},{4},{1},{4},{1}x", a((T - N) * b), a((E - L) * b), a((T + N) * b), a((E + L) * b), a(T * b)), n._.dirty = 1
                        }
                        if ("clip-rect" in l) {
                            var z = r(l["clip-rect"]).split(u);
                            if (4 == z.length) {
                                z[2] = +z[2] + +z[0], z[3] = +z[3] + +z[1];
                                var P = c.clipRect || t._g.doc.createElement("div"),
                                        F = P.style;
                                F.clip = t.format("rect({1}px {2}px {3}px {0}px)", z), c.clipRect || (F.position = "absolute", F.top = 0, F.left = 0, F.width = n.paper.width + "px", F.height = n.paper.height + "px", c.parentNode.insertBefore(P, c), P.appendChild(c), c.clipRect = P)
                            }
                            l["clip-rect"] || c.clipRect && (c.clipRect.style.clip = "auto")
                        }
                        if (n.textpath) {
                            var R = n.textpath.style;
                            l.font && (R.font = l.font), l["font-family"] && (R.fontFamily = '"' + l["font-family"].split(",")[0].replace(/^['"]+|['"]+$/g, d) + '"'), l["font-size"] && (R.fontSize = l["font-size"]), l["font-weight"] && (R.fontWeight = l["font-weight"]), l["font-style"] && (R.fontStyle = l["font-style"])
                        }
                        if ("arrow-start" in l && S(m, l["arrow-start"]), "arrow-end" in l && S(m, l["arrow-end"], 1), null != l.opacity || null != l.fill || null != l.src || null != l.stroke || null != l["stroke-width"] || null != l["stroke-opacity"] || null != l["fill-opacity"] || null != l["stroke-dasharray"] || null != l["stroke-miterlimit"] || null != l["stroke-linejoin"] || null != l["stroke-linecap"]) {
                            var I = c.getElementsByTagName(h),
                                    j = !1;
                            if (I = I && I[0], !I && (j = I = M(h)), "image" == n.type && l.src && (I.src = l.src), l.fill && (I.on = !0), null != I.on && "none" != l.fill && null !== l.fill || (I.on = !1), I.on && l.fill) {
                                var q = r(l.fill).match(t._ISURL);
                                if (q) {
                                    I.parentNode == c && c.removeChild(I), I.rotate = !0, I.src = q[1], I.type = "tile";
                                    var D = n.getBBox(1);
                                    I.position = D.x + p + D.y, n._.fillpos = [D.x, D.y], t._preload(q[1], function () {
                                        n._.fillsize = [this.offsetWidth, this.offsetHeight]
                                    })
                                } else I.color = t.getRGB(l.fill).hex, I.src = d, I.type = "solid", t.getRGB(l.fill).error && (m.type in {
                                    circle: 1,
                                    ellipse: 1
                                } || "r" != r(l.fill).charAt()) && A(m, l.fill, I) && (f.fill = "none", f.gradient = l.fill, I.rotate = !1)
                            }
                            if ("fill-opacity" in l || "opacity" in l) {
                                var V = ((+f["fill-opacity"] + 1 || 2) - 1) * ((+f.opacity + 1 || 2) - 1) * ((+t.getRGB(l.fill).o + 1 || 2) - 1);
                                V = o(s(V, 0), 1), I.opacity = V, I.src && (I.color = "none")
                            }
                            c.appendChild(I);
                            var O = c.getElementsByTagName("stroke") && c.getElementsByTagName("stroke")[0],
                                    Y = !1;
                            !O && (Y = O = M("stroke")), (l.stroke && "none" != l.stroke || l["stroke-width"] || null != l["stroke-opacity"] || l["stroke-dasharray"] || l["stroke-miterlimit"] || l["stroke-linejoin"] || l["stroke-linecap"]) && (O.on = !0), ("none" == l.stroke || null === l.stroke || null == O.on || 0 == l.stroke || 0 == l["stroke-width"]) && (O.on = !1);
                            var W = t.getRGB(l.stroke);
                            O.on && l.stroke && (O.color = W.hex), V = ((+f["stroke-opacity"] + 1 || 2) - 1) * ((+f.opacity + 1 || 2) - 1) * ((+W.o + 1 || 2) - 1);
                            var G = .75 * (i(l["stroke-width"]) || 1);
                            if (V = o(s(V, 0), 1), null == l["stroke-width"] && (G = f["stroke-width"]), l["stroke-width"] && (O.weight = G), G && 1 > G && (V *= G) && (O.weight = 1), O.opacity = V, l["stroke-linejoin"] && (O.joinstyle = l["stroke-linejoin"] || "miter"), O.miterlimit = l["stroke-miterlimit"] || 8, l["stroke-linecap"] && (O.endcap = "butt" == l["stroke-linecap"] ? "flat" : "square" == l["stroke-linecap"] ? "square" : "round"), "stroke-dasharray" in l) {
                                var H = {
                                    "-": "shortdash",
                                    ".": "shortdot",
                                    "-.": "shortdashdot",
                                    "-..": "shortdashdotdot",
                                    ". ": "dot",
                                    "- ": "dash",
                                    "--": "longdash",
                                    "- .": "dashdot",
                                    "--.": "longdashdot",
                                    "--..": "longdashdotdot"
                                };
                                O.dashstyle = H[e](l["stroke-dasharray"]) ? H[l["stroke-dasharray"]] : d
                            }
                            Y && c.appendChild(O)
                        }
                        if ("text" == m.type) {
                            m.paper.canvas.style.display = d;
                            var X = m.paper.span,
                                    U = 100,
                                    $ = f.font && f.font.match(/\d+(?:\.\d*)?(?=px)/);
                            g = X.style, f.font && (g.font = f.font), f["font-family"] && (g.fontFamily = f["font-family"]), f["font-weight"] && (g.fontWeight = f["font-weight"]), f["font-style"] && (g.fontStyle = f["font-style"]), $ = i(f["font-size"] || $ && $[0]) || 10, g.fontSize = $ * U + "px", m.textpath.string && (X.innerHTML = r(m.textpath.string).replace(/</g, "&#60;").replace(/&/g, "&#38;").replace(/\n/g, "<br>"));
                            var Z = X.getBoundingClientRect();
                            m.W = f.w = (Z.right - Z.left) / U, m.H = f.h = (Z.bottom - Z.top) / U, m.X = f.x, m.Y = f.y + m.H / 2, ("x" in l || "y" in l) && (m.path.v = t.format("m{0},{1}l{2},{1}", a(f.x * b), a(f.y * b), a(f.x * b) + 1));
                            for (var Q = ["x", "y", "text", "font", "font-family", "font-weight", "font-style", "font-size"], J = 0, K = Q.length; K > J; J++)
                                if (Q[J] in l) {
                                    m._.dirty = 1;
                                    break
                                }
                            switch (f["text-anchor"]) {
                                case "start":
                                    m.textpath.style["v-text-align"] = "left", m.bbx = m.W / 2;
                                    break;
                                case "end":
                                    m.textpath.style["v-text-align"] = "right", m.bbx = -m.W / 2;
                                    break;
                                default:
                                    m.textpath.style["v-text-align"] = "center", m.bbx = 0
                            }
                            m.textpath.style["v-text-kern"] = !0
                        }
                    }, A = function (e, a, s) {
                        e.attrs = e.attrs || {};
                        var o = e.attrs,
                                l = Math.pow,
                                h, u, c = "linear",
                                f = ".5 .5";
                        if (e.attrs.gradient = a, a = r(a).replace(t._radial_gradient, function (t, e, r) {
                            return c = "radial", e && r && (e = i(e), r = i(r), l(e - .5, 2) + l(r - .5, 2) > .25 && (r = n.sqrt(.25 - l(e - .5, 2)) * (2 * (r > .5) - 1) + .5), f = e + p + r), d
                        }), a = a.split(/\s*\-\s*/), "linear" == c) {
                            var g = a.shift();
                            if (g = -i(g), isNaN(g)) return null
                        }
                        var x = t._parseDots(a);
                        if (!x) return null;
                        if (e = e.shape || e.node, x.length) {
                            e.removeChild(s), s.on = !0, s.method = "none", s.color = x[0].color, s.color2 = x[x.length - 1].color;
                            for (var v = [], y = 0, m = x.length; m > y; y++) x[y].offset && v.push(x[y].offset + p + x[y].color);
                            s.colors = v.length ? v.join() : "0% " + s.color, "radial" == c ? (s.type = "gradientTitle", s.focus = "100%", s.focussize = "0 0", s.focusposition = f, s.angle = 0) : (s.type = "gradient", s.angle = (270 - g) % 360), e.appendChild(s)
                        }
                        return 1
                    }, E = function (e, r) {
                        this[0] = this.node = e, e.raphael = !0, this.id = t._oid++, e.raphaelid = this.id, this.X = 0, this.Y = 0, this.attrs = {}, this.paper = r, this.matrix = t.matrix(), this._ = {
                            transform: [],
                            sx: 1,
                            sy: 1,
                            dx: 0,
                            dy: 0,
                            deg: 0,
                            dirty: 1,
                            dirtyT: 1
                        }, !r.bottom && (r.bottom = this), this.prev = r.top, r.top && (r.top.next = this), r.top = this, this.next = null
                    }, N = t.el;
                    E.prototype = N, N.constructor = E, N.transform = function (e) {
                        if (null == e) return this._.transform;
                        var i = this.paper._viewBoxShift,
                            n = i ? "s" + [i.scale, i.scale] + "-1-1t" + [i.dx, i.dy] : d,
                            a;
                        i && (a = e = r(e).replace(/\.{3}|\u2026/g, this._.transform || d)), t._extractTransform(this, n + e);
                        var s = this.matrix.clone(),
                            o = this.skew,
                            l = this.node,
                            h, u = ~r(this.attrs.fill).indexOf("-"),
                            c = !r(this.attrs.fill).indexOf("url(");
                        if (s.translate(1, 1), c || u || "image" == this.type)
                            if (o.matrix = "1 0 0 1", o.offset = "0 0", h = s.split(), u && h.noRotation || !h.isSimple) {
                                l.style.filter = s.toFilter();
                                var f = this.getBBox(),
                                    g = this.getBBox(1),
                                    x = f.x - g.x,
                                    v = f.y - g.y;
                                l.coordorigin = x * -b + p + v * -b, C(this, 1, 1, x, v, 0)
                            } else l.style.filter = d, C(this, h.scalex, h.scaley, h.dx, h.dy, h.rotate);
                        else l.style.filter = d, o.matrix = r(s), o.offset = s.offset();
                        return null !== a && (this._.transform = a, t._extractTransform(this, a)), this
                    }, N.rotate = function (t, e, n) {
                        if (this.removed) return this;
                        if (null != t) {
                            if (t = r(t).split(u), t.length - 1 && (e = i(t[1]), n = i(t[2])), t = i(t[0]), null == n && (e = n), null == e || null == n) {
                                var a = this.getBBox(1);
                                e = a.x + a.width / 2, n = a.y + a.height / 2
                            }
                            return this._.dirtyT = 1, this.transform(this._.transform.concat([
                                ["r", t, e, n]
                            ])), this
                        }
                    }, N.translate = function (t, e) {
                        return this.removed ? this : (t = r(t).split(u), t.length - 1 && (e = i(t[1])), t = i(t[0]) || 0, e = +e || 0, this._.bbox && (this._.bbox.x += t, this._.bbox.y += e), this.transform(this._.transform.concat([
                            ["t", t, e]
                        ])), this)
                    }, N.scale = function (t, e, n, a) {
                        if (this.removed) return this;
                        if (t = r(t).split(u), t.length - 1 && (e = i(t[1]), n = i(t[2]), a = i(t[3]), isNaN(n) && (n = null), isNaN(a) && (a = null)), t = i(t[0]), null == e && (e = t), null == a && (n = a), null == n || null == a) var s = this.getBBox(1);
                        return n = null == n ? s.x + s.width / 2 : n, a = null == a ? s.y + s.height / 2 : a, this.transform(this._.transform.concat([
                            ["s", t, e, n, a]
                        ])), this._.dirtyT = 1, this
                    }, N.hide = function () {
                        return !this.removed && (this.node.style.display = "none"), this
                    }, N.show = function () {
                        return !this.removed && (this.node.style.display = d), this
                    }, N.auxGetBBox = t.el.getBBox, N.getBBox = function () {
                        var t = this.auxGetBBox();
                        if (this.paper && this.paper._viewBoxShift) {
                            var e = {}, r = 1 / this.paper._viewBoxShift.scale;
                            return e.x = t.x - this.paper._viewBoxShift.dx, e.x *= r, e.y = t.y - this.paper._viewBoxShift.dy, e.y *= r, e.width = t.width * r, e.height = t.height * r, e.x2 = e.x + e.width, e.y2 = e.y + e.height, e
                        }
                        return t
                    }, N._getBBox = function () {
                        return this.removed ? {} : {
                            x: this.X + (this.bbx || 0) - this.W / 2,
                            y: this.Y - this.H,
                            width: this.W,
                            height: this.H
                        }
                    }, N.remove = function () {
                        if (!this.removed && this.node.parentNode) {
                            this.paper.__set__ && this.paper.__set__.exclude(this), t.eve.unbind("raphael.*.*." + this.id), t._tear(this, this.paper), this.node.parentNode.removeChild(this.node), this.shape && this.shape.parentNode.removeChild(this.shape);
                            for (var e in this) this[e] = "function" == typeof this[e] ? t._removedFactory(e) : null;
                            this.removed = !0
                        }
                    }, N.attr = function (r, i) {
                        if (this.removed) return this;
                        if (null == r) {
                            var n = {};
                            for (var a in this.attrs) this.attrs[e](a) && (n[a] = this.attrs[a]);
                            return n.gradient && "none" == n.fill && (n.fill = n.gradient) && delete n.gradient, n.transform = this._.transform, n
                        }
                        if (null == i && t.is(r, "string")) {
                            if (r == h && "none" == this.attrs.fill && this.attrs.gradient) return this.attrs.gradient;
                            for (var s = r.split(u), o = {}, l = 0, f = s.length; f > l; l++) r = s[l], r in this.attrs ? o[r] = this.attrs[r] : t.is(this.paper.customAttributes[r], "function") ? o[r] = this.paper.customAttributes[r].def : o[r] = t._availableAttrs[r];
                            return f - 1 ? o : o[s[0]]
                        }
                        if (this.attrs && null == i && t.is(r, "array")) {
                            for (o = {}, l = 0, f = r.length; f > l; l++) o[r[l]] = this.attr(r[l]);
                            return o
                        }
                        var p;
                        null != i && (p = {}, p[r] = i), null == i && t.is(r, "object") && (p = r);
                        for (var d in p) c("raphael.attr." + d + "." + this.id, this, p[d]);
                        if (p) {
                            for (d in this.paper.customAttributes)
                                if (this.paper.customAttributes[e](d) && p[e](d) && t.is(this.paper.customAttributes[d], "function")) {
                                    var g = this.paper.customAttributes[d].apply(this, [].concat(p[d]));
                                    this.attrs[d] = p[d];
                                    for (var x in g) g[e](x) && (p[x] = g[x])
                                }
                            p.text && "text" == this.type && (this.textpath.string = p.text), T(this, p)
                        }
                        return this
                    }, N.toFront = function () {
                        return !this.removed && this.node.parentNode.appendChild(this.node), this.paper && this.paper.top != this && t._tofront(this, this.paper), this
                    }, N.toBack = function () {
                        return this.removed ? this : (this.node.parentNode.firstChild != this.node && (this.node.parentNode.insertBefore(this.node, this.node.parentNode.firstChild), t._toback(this, this.paper)), this)
                    }, N.insertAfter = function (e) {
                        return this.removed ? this : (e.constructor == t.st.constructor && (e = e[e.length - 1]), e.node.nextSibling ? e.node.parentNode.insertBefore(this.node, e.node.nextSibling) : e.node.parentNode.appendChild(this.node), t._insertafter(this, e, this.paper), this)
                    }, N.insertBefore = function (e) {
                        return this.removed ? this : (e.constructor == t.st.constructor && (e = e[0]), e.node.parentNode.insertBefore(this.node, e.node), t._insertbefore(this, e, this.paper), this)
                    }, N.blur = function (e) {
                        var r = this.node.runtimeStyle,
                            i = r.filter;
                        return i = i.replace(v, d), 0 !== +e ? (this.attrs.blur = e, r.filter = i + p + f + ".Blur(pixelradius=" + (+e || 1.5) + ")", r.margin = t.format("-{0}px 0 0 -{0}px", a(+e || 1.5))) : (r.filter = i, r.margin = 0, delete this.attrs.blur), this
                    }, t._engine.path = function (t, e) {
                        var r = M("shape");
                        r.style.cssText = m, r.coordsize = b + p + b, r.coordorigin = e.coordorigin;
                        var i = new E(r, e),
                            n = {
                                fill: "none",
                                stroke: "#000"
                            };
                        t && (n.path = t), i.type = "path", i.path = [], i.Path = d, T(i, n), e.canvas && e.canvas.appendChild(r);
                        var a = M("skew");
                        return a.on = !0, r.appendChild(a), i.skew = a, i.transform(d), i
                    }, t._engine.rect = function (e, r, i, n, a, s) {
                        var o = t._rectPath(r, i, n, a, s),
                            l = e.path(o),
                            h = l.attrs;
                        return l.X = h.x = r, l.Y = h.y = i, l.W = h.width = n, l.H = h.height = a, h.r = s, h.path = o, l.type = "rect", l
                    }, t._engine.ellipse = function (t, e, r, i, n) {
                        var a = t.path(),
                            s = a.attrs;
                        return a.X = e - i, a.Y = r - n, a.W = 2 * i, a.H = 2 * n, a.type = "ellipse", T(a, {
                            cx: e,
                            cy: r,
                            rx: i,
                            ry: n
                        }), a
                    }, t._engine.circle = function (t, e, r, i) {
                        var n = t.path(),
                            a = n.attrs;
                        return n.X = e - i, n.Y = r - i, n.W = n.H = 2 * i, n.type = "circle", T(n, {
                            cx: e,
                            cy: r,
                            r: i
                        }), n
                    }, t._engine.image = function (e, r, i, n, a, s) {
                        var o = t._rectPath(i, n, a, s),
                            l = e.path(o).attr({
                                stroke: "none"
                            }),
                            u = l.attrs,
                            c = l.node,
                            f = c.getElementsByTagName(h)[0];
                        return u.src = r, l.X = u.x = i, l.Y = u.y = n, l.W = u.width = a, l.H = u.height = s, u.path = o, l.type = "image", f.parentNode == c && c.removeChild(f), f.rotate = !0, f.src = r, f.type = "tile", l._.fillpos = [i, n], l._.fillsize = [a, s], c.appendChild(f), C(l, 1, 1, 0, 0, 0), l
                    }, t._engine.text = function (e, i, n, s) {
                        var o = M("shape"),
                            l = M("path"),
                            h = M("textpath");
                        i = i || 0, n = n || 0, s = s || "", l.v = t.format("m{0},{1}l{2},{1}", a(i * b), a(n * b), a(i * b) + 1), l.textpathok = !0, h.string = r(s), h.on = !0, o.style.cssText = m, o.coordsize = b + p + b, o.coordorigin = "0 0";
                        var u = new E(o, e),
                            c = {
                                fill: "#000",
                                stroke: "none",
                                font: t._availableAttrs.font,
                                text: s
                            };
                        u.shape = o, u.path = l, u.textpath = h, u.type = "text", u.attrs.text = r(s), u.attrs.x = i, u.attrs.y = n, u.attrs.w = 1, u.attrs.h = 1, T(u, c), o.appendChild(h), o.appendChild(l), e.canvas.appendChild(o);
                        var f = M("skew");
                        return f.on = !0, o.appendChild(f), u.skew = f, u.transform(d), u
                    }, t._engine.setSize = function (e, r) {
                        var i = this.canvas.style;
                        return this.width = e, this.height = r, e == +e && (e += "px"), r == +r && (r += "px"), i.width = e, i.height = r, i.clip = "rect(0 " + e + " " + r + " 0)", this._viewBox && t._engine.setViewBox.apply(this, this._viewBox), this
                    }, t._engine.setViewBox = function (e, r, i, n, a) {
                        t.eve("raphael.setViewBox", this, this._viewBox, [e, r, i, n, a]);
                        var s = this.getSize(),
                            o = s.width,
                            l = s.height,
                            h, u;
                        return a && (h = l / n, u = o / i, o > i * h && (e -= (o - i * h) / 2 / h), l > n * u && (r -= (l - n * u) / 2 / u)), this._viewBox = [e, r, i, n, !!a], this._viewBoxShift = {
                            dx: -e,
                            dy: -r,
                            scale: s
                        }, this.forEach(function (t) {
                            t.transform("...")
                        }), this
                    };
                    var M;
                    t._engine.initWin = function (t) {
                        var e = t.document;
                        e.styleSheets.length < 31 ? e.createStyleSheet().addRule(".rvml", "behavior:url(#default#VML)") : e.styleSheets[0].addRule(".rvml", "behavior:url(#default#VML)");
                        try {
                            !e.namespaces.rvml && e.namespaces.add("rvml", "urn:schemas-microsoft-com:vml"), M = function (t) {
                                return e.createElement("<rvml:" + t + ' class="rvml">')
                            }
                        } catch (r) {
                            M = function (t) {
                                return e.createElement("<" + t + ' xmlns="urn:schemas-microsoft.com:vml" class="rvml">')
                            }
                        }
                    }, t._engine.initWin(t._g.win), t._engine.create = function () {
                        var e = t._getContainer.apply(0, arguments),
                            r = e.container,
                            i = e.height,
                            n, a = e.width,
                            s = e.x,
                            o = e.y;
                        if (!r) throw new Error("VML container not found.");
                        var l = new t._Paper,
                            h = l.canvas = t._g.doc.createElement("div"),
                            u = h.style;
                        return s = s || 0, o = o || 0, a = a || 512, i = i || 342, l.width = a, l.height = i, a == +a && (a += "px"), i == +i && (i += "px"), l.coordsize = 1e3 * b + p + 1e3 * b, l.coordorigin = "0 0", l.span = t._g.doc.createElement("span"), l.span.style.cssText = "position:absolute;left:-9999em;top:-9999em;padding:0;margin:0;line-height:1;", h.appendChild(l.span), u.cssText = t.format("top:0;left:0;width:{0};height:{1};display:inline-block;position:relative;clip:rect(0 {0} {1} 0);overflow:hidden", a, i), 1 == r ? (t._g.doc.body.appendChild(h), u.left = s + "px", u.top = o + "px", u.position = "absolute") : r.firstChild ? r.insertBefore(h, r.firstChild) : r.appendChild(h), l.renderfix = function () { }, l
                    }, t.prototype.clear = function () {
                        t.eve("raphael.clear", this), this.canvas.innerHTML = d, this.span = t._g.doc.createElement("span"), this.span.style.cssText = "position:absolute;left:-9999em;top:-9999em;padding:0;margin:0;line-height:1;display:inline;", this.canvas.appendChild(this.span), this.bottom = this.top = null
                    }, t.prototype.remove = function () {
                        t.eve("raphael.remove", this), this.canvas.parentNode.removeChild(this.canvas);
                        for (var e in this) this[e] = "function" == typeof this[e] ? t._removedFactory(e) : null;
                        return !0
                    };
                    var L = t.st;
                    for (var z in N) N[e](z) && !L[e](z) && (L[z] = function (t) {
                        return function () {
                            var e = arguments;
                            return this.forEach(function (r) {
                                r[t].apply(r, e)
                            })
                        }
                    } (z))
                }
            } .apply(e, i), !(void 0 !== n && (t.exports = n))
        }
    ])
});