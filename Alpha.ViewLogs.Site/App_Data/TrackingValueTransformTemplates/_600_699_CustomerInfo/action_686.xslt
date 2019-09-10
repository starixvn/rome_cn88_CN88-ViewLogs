<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="../helper.xslt" />
  <xsl:param name="IsInternal" select="0"></xsl:param>
  <xsl:output method="html" indent="no" />

  <xsl:template match="r">
    <table class="tblDesc">
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'username'" />
        <xsl:with-param name="firstValue" select="@subaccname" />

        <xsl:with-param name="secondKey" select="'Name'" />
        <xsl:with-param name="secondValue" select="@fname" />

        <xsl:with-param name="thirdKey" select="'language'" />
        <xsl:with-param name="thirdValue" select="@language" />
      </xsl:call-template>
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'Permissions'" />
        <xsl:with-param name="firstValue" select="@pageaccess" />
      </xsl:call-template>
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'IP'" />
        <xsl:with-param name="firstValue" select="@loginip" />

        <xsl:with-param name="secondKey" select="'errCode'" />
        <xsl:with-param name="secondValue" select="@errcode" />

        <xsl:with-param name="thirdKey" select="'errMsg'" />
        <xsl:with-param name="thirdValue" select="@errmsg" />
      </xsl:call-template>
      <xsl:if test="$IsInternal != 0">
        <xsl:call-template name="renderRow">
          <xsl:with-param name="firstKey" select="'subAccID'" />
          <xsl:with-param name="firstValue" select="@subaccid" />
        </xsl:call-template>
      </xsl:if>
    </table>
  </xsl:template>
</xsl:stylesheet>
