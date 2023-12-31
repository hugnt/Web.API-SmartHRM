import {
  useStore as n,
  useSelector as r,
  useState as e,
  className as t,
  useEffect as o,
} from "../grid.module.js";
var l, c, i;
function u(n, r, e, t, o) {
  var l = {
    type: n,
    props: r,
    key: e,
    ref: t,
    __k: null,
    __: null,
    __b: 0,
    __e: null,
    __d: void 0,
    __c: null,
    __h: null,
    constructor: void 0,
    __v: null == o ? ++i : o,
  };
  return null == o && null != c.vnode && c.vnode(l), l;
}
function a() {
  return (
    (a = Object.assign
      ? Object.assign.bind()
      : function (n) {
          for (var r = 1; r < arguments.length; r++) {
            var e = arguments[r];
            for (var t in e)
              Object.prototype.hasOwnProperty.call(e, t) && (n[t] = e[t]);
          }
          return n;
        }),
    a.apply(this, arguments)
  );
}
(l = [].slice),
  (c = {
    __e: function (n, r, e, t) {
      for (var o, l, c; (r = r.__); )
        if ((o = r.__c) && !o.__)
          try {
            if (
              ((l = o.constructor) &&
                null != l.getDerivedStateFromError &&
                (o.setState(l.getDerivedStateFromError(n)), (c = o.__d)),
              null != o.componentDidCatch &&
                (o.componentDidCatch(n, t || {}), (c = o.__d)),
              c)
            )
              return (o.__E = o);
          } catch (r) {
            n = r;
          }
      throw n;
    },
  }),
  (i = 0);
var d = function (n) {
    return function (r) {
      var e,
        t = (null == (e = r.rowSelection) ? void 0 : e.rowIds) || [];
      return t.indexOf(n) > -1
        ? r
        : a({}, r, { rowSelection: { rowIds: [n].concat(t) } });
    };
  },
  f = function (n) {
    return function (r) {
      var e,
        t = (null == (e = r.rowSelection) ? void 0 : e.rowIds) || [],
        o = t.indexOf(n);
      if (-1 === o) return r;
      var l = [].concat(t);
      return l.splice(o, 1), a({}, r, { rowSelection: { rowIds: l } });
    };
  },
  s = { __proto__: null, CheckRow: d, UncheckRow: f };
function _(c) {
  var i = this,
    a = n().dispatch,
    s = r(function (n) {
      return n.rowSelection;
    }),
    _ = e(!1),
    v = _[0],
    p = _[1],
    h = t("tr", "selected"),
    w = t("checkbox"),
    m = function (n) {
      return void 0 !== n.row;
    };
  o(function () {
    var n;
    null != (n = c.cell) && n.data && m(c) && b();
  }, []),
    o(
      function () {
        var n =
          i.base && i.base.parentElement && i.base.parentElement.parentElement;
        if (n) {
          var r =
            ((null == s ? void 0 : s.rowIds) || []).indexOf(c.row.id) > -1;
          p(r), r ? n.classList.add(h) : n.classList.remove(h);
        }
      },
      [s]
    );
  var b = function () {
    var n;
    a(d(c.row.id)), null == (n = c.cell) || n.update(!0);
  };
  return m(c)
    ? (function (n, r, e) {
        var t,
          o,
          c,
          i = {};
        for (c in r)
          "key" == c ? (t = r[c]) : "ref" == c ? (o = r[c]) : (i[c] = r[c]);
        if (
          (arguments.length > 2 &&
            (i.children = arguments.length > 3 ? l.call(arguments, 2) : e),
          "function" == typeof n && null != n.defaultProps)
        )
          for (c in n.defaultProps)
            void 0 === i[c] && (i[c] = n.defaultProps[c]);
        return u(n, i, t, o, null);
      })("input", {
        type: "checkbox",
        checked: v,
        onChange: function () {
          var n;
          v ? (a(f(c.row.id)), null == (n = c.cell) || n.update(!1)) : b();
        },
        className: w,
      })
    : null;
}
export { _ as RowSelection, s as RowSelectionActions };
//# sourceMappingURL=selection.module.js.map

