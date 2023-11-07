!(function (e, n) {
  "object" == typeof exports && "undefined" != typeof module
    ? n(exports, require("gridjs"))
    : "function" == typeof define && define.amd
    ? define(["exports", "gridjs"], n)
    : n(
        (((e || self).gridjs = e.gridjs || {}),
        (e.gridjs.plugins = e.gridjs.plugins || {}),
        (e.gridjs.plugins.selection = {})),
        e.gridjs
      );
})(this, function (e, n) {
  var t, r, o;
  function l(e, n, t, l, i) {
    var c = {
      type: e,
      props: n,
      key: t,
      ref: l,
      __k: null,
      __: null,
      __b: 0,
      __e: null,
      __d: void 0,
      __c: null,
      __h: null,
      constructor: void 0,
      __v: null == i ? ++o : i,
    };
    return null == i && null != r.vnode && r.vnode(c), c;
  }
  function i() {
    return (
      (i = Object.assign
        ? Object.assign.bind()
        : function (e) {
            for (var n = 1; n < arguments.length; n++) {
              var t = arguments[n];
              for (var r in t)
                Object.prototype.hasOwnProperty.call(t, r) && (e[r] = t[r]);
            }
            return e;
          }),
      i.apply(this, arguments)
    );
  }
  (t = [].slice),
    (r = {
      __e: function (e, n, t, r) {
        for (var o, l, i; (n = n.__); )
          if ((o = n.__c) && !o.__)
            try {
              if (
                ((l = o.constructor) &&
                  null != l.getDerivedStateFromError &&
                  (o.setState(l.getDerivedStateFromError(e)), (i = o.__d)),
                null != o.componentDidCatch &&
                  (o.componentDidCatch(e, r || {}), (i = o.__d)),
                i)
              )
                return (o.__E = o);
            } catch (n) {
              e = n;
            }
        throw e;
      },
    }),
    (o = 0);
  var c = function (e) {
      return function (n) {
        var t,
          r = (null == (t = n.rowSelection) ? void 0 : t.rowIds) || [];
        return r.indexOf(e) > -1
          ? n
          : i({}, n, { rowSelection: { rowIds: [e].concat(r) } });
      };
    },
    u = function (e) {
      return function (n) {
        var t,
          r = (null == (t = n.rowSelection) ? void 0 : t.rowIds) || [],
          o = r.indexOf(e);
        if (-1 === o) return n;
        var l = [].concat(r);
        return l.splice(o, 1), i({}, n, { rowSelection: { rowIds: l } });
      };
    },
    s = { __proto__: null, CheckRow: c, UncheckRow: u };
  (e.RowSelection = function (e) {
    var r = this,
      o = n.useStore().dispatch,
      i = n.useSelector(function (e) {
        return e.rowSelection;
      }),
      s = n.useState(!1),
      a = s[0],
      d = s[1],
      f = n.className("tr", "selected"),
      p = n.className("checkbox"),
      _ = function (e) {
        return void 0 !== e.row;
      };
    n.useEffect(function () {
      var n;
      null != (n = e.cell) && n.data && _(e) && v();
    }, []),
      n.useEffect(
        function () {
          var n =
            r.base &&
            r.base.parentElement &&
            r.base.parentElement.parentElement;
          if (n) {
            var t =
              ((null == i ? void 0 : i.rowIds) || []).indexOf(e.row.id) > -1;
            d(t), t ? n.classList.add(f) : n.classList.remove(f);
          }
        },
        [i]
      );
    var v = function () {
      var n;
      o(c(e.row.id)), null == (n = e.cell) || n.update(!0);
    };
    return _(e)
      ? (function (e, n, r) {
          var o,
            i,
            c,
            u = {};
          for (c in n)
            "key" == c ? (o = n[c]) : "ref" == c ? (i = n[c]) : (u[c] = n[c]);
          if (
            (arguments.length > 2 &&
              (u.children = arguments.length > 3 ? t.call(arguments, 2) : r),
            "function" == typeof e && null != e.defaultProps)
          )
            for (c in e.defaultProps)
              void 0 === u[c] && (u[c] = e.defaultProps[c]);
          return l(e, u, o, i, null);
        })("input", {
          type: "checkbox",
          checked: a,
          onChange: function () {
            var n;
            a ? (o(u(e.row.id)), null == (n = e.cell) || n.update(!1)) : v();
          },
          className: p,
        })
      : null;
  }),
    (e.RowSelectionActions = s);
});
//# sourceMappingURL=selection.umd.js.map

1;
